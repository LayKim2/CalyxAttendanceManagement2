using OpenAI;

namespace CalyxAttendanceManagement.Server.Services.OpenAIService
{
    public class OpenAIService : IOpenAIService
    {
        public async Task<ServiceResponse<string>> ResponseOpenAI(string prompt)
        {
            var response = new ServiceResponse<string>();

            var api = new OpenAIAPI(apiKeys: "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", engine: Engine.Davinci);

            var request = new CompletionRequestBuilder()
                .WithPrompt(prompt)
                .WithMaxTokens(100)
                .Build();

            var result = await api.Completions.CreateCompletionAsync(request);

            response.Data = result.ToString();

            return response;
        }
    }
}
