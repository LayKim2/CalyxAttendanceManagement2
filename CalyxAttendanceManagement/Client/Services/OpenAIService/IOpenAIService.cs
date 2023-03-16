namespace CalyxAttendanceManagement.Client.Services.OpenAIService;

public interface IOpenAIService
{
    public Task<string> ResponseOpenAI(string prompt);
}
