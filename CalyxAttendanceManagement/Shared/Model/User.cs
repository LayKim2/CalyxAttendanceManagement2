using System.ComponentModel.DataAnnotations;

namespace CalyxAttendanceManagement.Shared.Model;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Belong { get; set; } = string.Empty;
    public DateTime StartWorkDate { get; set; } = DateTime.Now;
    public string PhoneNumber { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateUpdated{ get; set; } = DateTime.Now;
    public string key { get; set; } = string.Empty;
    public bool IsAuthenticated { get; set; } = false;
    public string Role { get; set; } = "Employee";
    public UserPTO UserPTO { get; set; }
}
