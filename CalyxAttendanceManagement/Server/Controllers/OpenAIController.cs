using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalyxAttendanceManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;

        public OpenAIController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpGet("response_openai/{prompt}")]
        public async Task<ActionResult<ServiceResponse<string>>> ResponseOpenAI(string prompt)
        {
            return await _openAIService.ResponseOpenAI(prompt);
        }
    }
}
