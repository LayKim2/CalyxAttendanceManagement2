using System.ComponentModel.DataAnnotations;

namespace CalyxAttendanceManagement.Shared.ViewModel;

public class SendEmail
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
}
