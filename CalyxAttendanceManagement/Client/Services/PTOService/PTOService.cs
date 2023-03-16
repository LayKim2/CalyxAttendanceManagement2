using CalyxAttendanceManagement.Client.Pages.User;
using System.Net.Http.Json;

namespace CalyxAttendanceManagement.Client.Services.PTOService;

public class PTOService : IPTOService
{
    private readonly HttpClient _http;

    public IList<UserPTOHistory> UserPTOHistories { get; set; } = new List<UserPTOHistory>();
    public UserRequestPTO UserRequestPTO { get; set; } = new UserRequestPTO();
    public decimal UserPTOCount { get; set; }

    public PTOService(HttpClient http)
    {
        _http = http;
    }

    public event Action OnChange;

    public async Task GetUserPTOHistories()
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<IList<UserPTOHistory>>>("api/pto/get-histories");

        if (response.Success)
            UserPTOHistories = response.Data;
    }

    public async Task GetPTOCount()
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<decimal>>("api/pto/get-pto-count");
       
        UserPTOCount = response.Data;
    }

    public async Task<ServiceResponse<bool>> RequestPTO(UserRequestPTO request)
    {
        var result = await _http.PostAsJsonAsync("api/pto/request-pto", request);

        return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    //public async Task ClearUserRequestPTO()
    //{
    //    var resetModel = new UserRequestPTO()
    //    {
    //        CurrentPTOCount = UserPTOCount,
    //        NeedPTOCount = 0.00M,
    //        CalculatedPTOCount = UserPTOCount
    //    };

    //    UserRequestPTO = resetModel;

    //    OnChange.Invoke();
    //}
}
