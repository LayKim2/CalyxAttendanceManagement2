namespace CalyxAttendanceManagement.Server.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<ServiceResponse<string>> Login(string email, string password);
    Task<bool> UserExists(string email);
    Task<ServiceResponse<bool>> VerifyEmail(string email, string key);
    int GetUserId();
    string GetUserEmail();
    Task<User> GetUserByEmail(string email);
    Task<ServiceResponse<User>> GetUser();
    Task<ServiceResponse<List<User>>> GetUsers();
    Task<ServiceResponse<bool>> UpdateProfile(int userId, UpdateProfile profile);
    Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
    Task<ServiceResponse<List<VerifyUserPTO>>> GetVerifyPTO();
    Task<ServiceResponse<bool>> UpdateVerifyPTO(int historyId, bool result);
}
