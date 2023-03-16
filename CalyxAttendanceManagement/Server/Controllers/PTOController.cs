using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalyxAttendanceManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PTOController : ControllerBase
    {
        private IPTOService _ptoService;

        public PTOController(IPTOService ptoService)
        {
            _ptoService = ptoService;
        }

        [HttpGet("get-histories"), Authorize]
        public async Task<ActionResult<ServiceResponse<IList<UserPTOHistory>>>> GetUserPTOHistories()
        {
            return await _ptoService.GetPTOHistories();
        }

        [HttpGet("get-pto-count"), Authorize]
        public async Task<ActionResult<ServiceResponse<decimal>>> GetPTOCount()
        {
            return await _ptoService.GetPTOCount();
        }

        [HttpPost("request-pto"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> RequestPTO([FromBody] UserRequestPTO request)
        {
            return await _ptoService.RequestPTO(request);
        }
    }
}
