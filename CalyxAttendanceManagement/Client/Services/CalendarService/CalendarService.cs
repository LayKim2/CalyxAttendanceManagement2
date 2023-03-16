namespace CalyxAttendanceManagement.Client.Services.CalendarService;

public class CalendarService : ICalendarService
{
    private readonly HttpClient _http;

    public IList<Calendar> CalendarList { get; set; } = new List<Calendar>();
    public CalendarService(HttpClient http)
    {
        _http = http;
    }

    public event Action OnChange;

    public async Task GetCalendar()
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<IList<Calendar>>>("api/calendar");

        CalendarList = response.Data;
    }

}
