namespace CalyxAttendanceManagement.Server.Services.PTOService;

public interface IPTOService
{
    Task<ServiceResponse<IList<UserPTOHistory>>> GetPTOHistories();
    Task<ServiceResponse<decimal>> GetPTOCount();
    Task<ServiceResponse<bool>> RequestPTO(UserRequestPTO applyPTO);
}
