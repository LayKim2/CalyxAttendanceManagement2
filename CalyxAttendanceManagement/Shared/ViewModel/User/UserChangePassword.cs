using System.ComponentModel.DataAnnotations;

namespace CalyxAttendanceManagement.Shared.ViewModel;

public class UserChangePassword
{
    [Required, StringLength(20, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "The password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

