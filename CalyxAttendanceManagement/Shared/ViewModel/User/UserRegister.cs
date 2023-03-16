using System.ComponentModel.DataAnnotations;

namespace CalyxAttendanceManagement.Shared.ViewModel;

public class UserRegister
{
    [Required, EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@calyxsoftware.com$", ErrorMessage = "Email address must be from 'calyxsoftware.com'")]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Belong { get; set; } = string.Empty;
    [Required]
    public DateTime? StartWorkDate { get; set; } = DateTime.Now;

    [Required, StringLength(50, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

}
