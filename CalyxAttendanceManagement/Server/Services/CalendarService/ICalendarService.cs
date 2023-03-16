namespace CalyxAttendanceManagement.Server.Services.CalendarService;

public interface ICalendarService
{
    Task<ServiceResponse<IList<Calendar>>> GetCalendar();
    Task<ServiceResponse<bool>> AddCalendar(Calendar calendar);
}
