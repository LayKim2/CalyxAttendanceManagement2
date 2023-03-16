using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Security.Claims;

namespace CalyxAttendanceManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
        {
            var response = await _authService.Register(
                new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Belong = request.Belong,
                    StartWorkDate = request.StartWorkDate?? DateTime.Now
                },
                request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLogin request)
        {
            var response = await _authService.Login(request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("verify_email")]
        public async Task<ActionResult> VerifyEmail(string email, string key)
        {
            var response = await _authService.VerifyEmail(email, key);

            //if (!response.Success)
            //{
            //    return BadRequest(response);
            //}

            //return Ok(response);

            return RedirectToPage("/verified");
        }

        [HttpGet("get-user"), Authorize]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser()
        {
            return await _authService.GetUser();
        }

        [HttpGet("get-users"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUsers()
        {
            return await _authService.GetUsers();
        }

        [HttpPost("update-profile"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateProfile([FromBody] UpdateProfile profile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.UpdateProfile(int.Parse(userId), profile);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPasword)
        {
            // basecontroller의 User class( Identity ) - not db user table
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePassword(int.Parse(userId), newPasword);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("get-verify-pto"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<VerifyUserPTO>>>> GetVerifyPTO()
        {
            return await _authService.GetVerifyPTO();
        }   

        [HttpPost("update-verify-pto"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateVerifyPTO([FromBody] UpdateUserPTO updateUserPTO)
        {
            var response = await _authService.UpdateVerifyPTO(updateUserPTO.UserPTOHistoryId, updateUserPTO.Result);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }

}
