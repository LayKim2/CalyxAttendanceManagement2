
namespace CalyxAttendanceManagement.Shared.ViewModel;

public class VerifyUserPTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Belong { get; set; } = string.Empty;
    public decimal Pto { get; set; }
    public string PTOType { get; set; } = string.Empty;
    public string CountType { get; set; } = string.Empty;
    public decimal Count { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime? Date { get; set; } = DateTime.Now;
    public DateTime? CreatedDate { get; set; } = DateTime.Now;
}
