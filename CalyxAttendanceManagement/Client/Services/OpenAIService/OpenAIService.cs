using System.Web;

namespace CalyxAttendanceManagement.Client.Services.OpenAIService;

public class OpenAIService : IOpenAIService
{
    private readonly HttpClient _http;

    public OpenAIService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> ResponseOpenAI(string prompt)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<string>>($"api/openai/response_openai/{prompt}");

        return response.Data;
    }

}
