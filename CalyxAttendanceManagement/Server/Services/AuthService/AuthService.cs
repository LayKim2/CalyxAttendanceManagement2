using CalyxAttendanceManagement.Shared.Model;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CalyxAttendanceManagement.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

        public Task<User> GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else if (!user.IsAuthenticated)
            {
                response.Success = false;
                response.Message = "You need to verify email.";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int> { 
                    Success = false,
                    Message = "User already exists."
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Name = user.FirstName + " " + user.LastName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.key = Guid.NewGuid().ToString();

            if (user.Email == "wayne_kim@calyxsoftware.com")
                user.Role = "Admin";

            _context.Users.Add(user);

            UserPTO userPto = new UserPTO()
            {
                UserId = user.Id,
                Pto = 0.00M,
            };

            _context.UserPTO.Add(userPto);

            await _context.SaveChangesAsync();

            await SendEmail(user);

            return new ServiceResponse<int> { Data = user.Id, Message = "Registration successful and Sent email for verify." };
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(user => user.Email.ToLower()
                .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // cryptography algorithm
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role) // TODO -- token role
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(9),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> VerifyEmail(string email, string key)
        {
            var response = new ServiceResponse<bool>();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()) && x.key == key);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else
            {
                user.IsAuthenticated = true;

                await _context.SaveChangesAsync();

                response.Success = true;
            }

            return response;
        }

        private async Task<bool> SendEmail(User user)
        {
            var apiKey = _configuration.GetSection("SendGridSettings:Key").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_configuration.GetSection("SendGridSettings:UserEmail").Value, _configuration.GetSection("SendGridSettings:UserName").Value);
            var to = new EmailAddress(user.Email, user.Name);

            var templateId = _configuration.GetSection("SendGridSettings:VerifyTemplate").Value;

            var dynamicTemplateData = new
            {
                subject = "Verify Email Address for Calyx Attendance Management",
                sender_name = user.Name,
                sender_email = user.Email,
                key = user.key
            };

            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);
            var response = await client.SendEmailAsync(msg);

            return true;
        }

        public async Task<ServiceResponse<User>> GetUser()
        {
            var id = GetUserId();
            var email = GetUserEmail();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Id.Equals(id));

            return new ServiceResponse<User> { Data = user };
        }

        public async Task<ServiceResponse<List<User>>> GetUsers()
        {
            var id = GetUserId();
            var email = GetUserEmail();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Id.Equals(id));

            if( user == null)
            {
                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = "Sorry, but user does not exist.",
                };
            } else if (!_httpContextAccessor.HttpContext.User.IsInRole("Admin") || user.Role != "Admin")
            {
                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = "Sorry. This data only can see admin.",
                };
            } else
            {
                var users = await _context.Users
                                .Include(u => u.UserPTO)
                                .ThenInclude(p => p.UserPtoHistory)
                                .ToListAsync();

                foreach (var u in users)
                {
                    if(u.UserPTO == null)
                        u.UserPTO = new UserPTO();

                    if(u.UserPTO.UserPtoHistory == null)
                        u.UserPTO.UserPtoHistory = new List<UserPTOHistory>();

                    foreach (var data in u.UserPTO.UserPtoHistory)
                    {
                        switch (data.PTOType)
                        {
                            case "1일 이상":
                                data.Date = data.StartDate.Value.ToString("MM/dd/yyyy") + " ~ " + data.EndDate.Value.ToString("MM/dd/yyyy");
                                break;
                            default:
                                data.Date = data.StartDate.Value.ToString("MM/dd/yyyy");
                                break;
                        }
                    }
                }

                return new ServiceResponse<List<User>> { Data = users };
            }
        }

        public async Task<ServiceResponse<List<VerifyUserPTO>>> GetVerifyPTO()
        {
            List<VerifyUserPTO> verifyUserPTO;

            verifyUserPTO = (from uph in _context.UserPTOHistory
                            join up in _context.UserPTO on uph.UserPTOId equals up.Id
                            join u in _context.Users on uph.UserId equals u.Id
                            where uph.VerifiedType == "Pending"
                            orderby uph.CreatedDate
                            select new VerifyUserPTO
                            {
                                Id = uph.Id,
                                Name = u.Name,
                                Email = u.Email,
                                Belong = u.Belong,
                                Pto = up.Pto,
                                PTOType = uph.PTOType,
                                Count = uph.NeedPTO,
                                Comment = uph.Comment,
                                CreatedDate = uph.CreatedDate
                            }).ToList();

            return new ServiceResponse<List<VerifyUserPTO>>
            {
                Data = verifyUserPTO
            };
        }

        public async Task<ServiceResponse<bool>> UpdateProfile(int userId, UpdateProfile profile)
        {
            var email = GetUserEmail();

            var user = await _context.Users.Where(u => u.Id.Equals(userId) && u.Email.Equals(email)).FirstOrDefaultAsync();

            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Save failed.",
                };
            }

            user.Name = profile.UserName;
            user.PhoneNumber = profile.PhoneNumber;
            //user.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Profile is saved." };
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found.",
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Password has been changed." };
        }

        private async Task<bool> VerifyEmail(SendEmail request, UserPTOHistory userPTOHistory)
        {
            var apiKey = _configuration.GetSection("SendGridSettings:Key").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_configuration.GetSection("SendGridSettings:UserEmail").Value, _configuration.GetSection("SendGridSettings:UserName").Value);
            var to = new EmailAddress(request.Email, request.Name);
            var subject = "PTO Result";
            var plainTextContent = "";
            var datehtml = string.Empty;

            if (userPTOHistory.PTOType == "1일 이상")
            {
                datehtml = userPTOHistory.StartDate.Value.ToString("MM/dd/yyyy") + "~" + userPTOHistory.EndDate.Value.ToString("MM/dd/yyyy");
            }
            else
            {
                datehtml = userPTOHistory.StartDate.Value.ToString("MM/dd/yyyy");
            };

            var htmlContent = $"Hi, {request.Name} <br/></br/> <strong>Result : {userPTOHistory.VerifiedType}</strong> <br/></br/>  신청자 : {request.Name}, {request.Email} <br/><br/> PTO Type : {userPTOHistory.PTOType} <br/><br/> Date : {datehtml} <br/><br/> {userPTOHistory.Comment} <br/></br/></br/> Thank you";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

            return true;
        }

        public async Task<ServiceResponse<bool>> UpdateVerifyPTO(int historyId, bool result)
        {
            var id = GetUserId();
            var email = GetUserEmail();

            var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Id.Equals(id));

            if (adminUser == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Sorry, but user does not exist. please login again.",
                };
            }
            else if (!_httpContextAccessor.HttpContext.User.IsInRole("Admin") || adminUser.Role != "Admin")
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Sorry. This data only can be updated by admin.",
                };
            }
            else
            {
                var UserPTOHistory = await _context.UserPTOHistory.Where(u => u.Id == historyId).FirstOrDefaultAsync();

                if (UserPTOHistory == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Sorry. This data does not exist.",
                    };
                } else
                {
                    var userId = UserPTOHistory.UserId;

                    var userPto = await _context.UserPTO.Where(up => up.UserId == UserPTOHistory.UserId && up.Id == UserPTOHistory.UserPTOId).FirstOrDefaultAsync();

                    if(userPto == null) {
                        return new ServiceResponse<bool>
                        {
                            Success = false,
                            Message = "Sorry. This data does not exist.",
                        };
                    } else
                    {
                        if (result)
                        {
                            userPto.Pto = userPto.Pto - UserPTOHistory.NeedPTO;

                            UserPTOHistory.VerifiedType = "Accepted";
                            UserPTOHistory.VerifiedDate = DateTime.Now;

                            var calendar = new Calendar()
                            {
                                UserId = userId,
                                Start = UserPTOHistory.StartDate,
                                End = UserPTOHistory.EndDate,
                                Text = "[PTO] " + UserPTOHistory.PTOType
                            };

                            _context.Add(calendar);
                        } else
                        {
                            UserPTOHistory.VerifiedType = "Rejected";
                            UserPTOHistory.VerifiedDate = DateTime.Now;
                        }

                        await _context.SaveChangesAsync();

                        // send email
                        var user = await _context.Users.Where(u => u.Id == UserPTOHistory.UserId).FirstOrDefaultAsync();

                        if (user != null)
                        {
                            await VerifyEmail(new SendEmail { Email = user.Email, Name = user.Name }, UserPTOHistory);
                        }

                        return new ServiceResponse<bool>
                        {
                            Success = true,
                            Message = "Data saved.",
                        };
                    }
                }
            }
        }
    }
}
