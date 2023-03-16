using System.ComponentModel.DataAnnotations;

namespace CalyxAttendanceManagement.Shared.ViewModel;

public class UserRequestPTO
{
    [Required]
    public string PTOType { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }
    public decimal? CurrentPTOCount { get; set; }
    public decimal? NeedPTOCount { get; set; }
    public decimal? CalculatedPTOCount { get; set; }
    public string Comment { get; set; } = string.Empty;
}
