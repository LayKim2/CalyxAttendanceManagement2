namespace CalyxAttendanceManagement.Server.Services.OpenAIService
{
    public interface IOpenAIService
    {
        Task<ServiceResponse<string>> ResponseOpenAI(string prompt);
    }
}
