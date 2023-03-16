namespace CalyxAttendanceManagement.Server.Services.CalendarService;

public class CalendarService : ICalendarService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;

    public CalendarService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<IList<Calendar>>> GetCalendar()
    {
        int userId = _authService.GetUserId();

        var calendars = await _context.Calendar.Where(a => a.UserId == userId).ToListAsync();

        return new ServiceResponse<IList<Calendar>> { Data = calendars };
    }

    public async Task<ServiceResponse<bool>> AddCalendar(Calendar calendar)
    {
        var response = new ServiceResponse<bool>();

        try
        {
            calendar.UserId = _authService.GetUserId();

            _context.Calendar.Add(calendar);

            await _context.SaveChangesAsync();

            return response;
        }
        catch (Exception e)
        {
            return new ServiceResponse<bool>()
            {
                Success = false,
                Message = "Schedule save failed."
            };
        }
    }
}
