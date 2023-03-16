using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radzen.Blazor.Rendering;

namespace CalyxAttendanceManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<ServiceResponse<IList<Calendar>>>> GetCalendar()
        {
            return await _calendarService.GetCalendar();
        }

        [HttpPost("add-calendar"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> AddCalendar(Calendar calendar)
        {
            return await _calendarService.AddCalendar(calendar);
        }
    }
}
