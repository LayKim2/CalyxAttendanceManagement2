using Radzen.Blazor.Rendering;

namespace CalyxAttendanceManagement.Client.Services.CalendarService;

public interface ICalendarService
{
    event Action OnChange;

    IList<Calendar> CalendarList { get; set; }
    public Task GetCalendar();
}
