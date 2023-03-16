
using System.ComponentModel.DataAnnotations.Schema;

namespace CalyxAttendanceManagement.Shared.Model;

public class UserPTO
{
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public decimal Pto { get; set; }
    public IEnumerable<UserPTOHistory> UserPtoHistory { get; set; } = new List<UserPTOHistory>();
}
