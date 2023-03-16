
namespace CalyxAttendanceManagement.Shared.Model;

public class Calendar
{
    public int Id { get; set; }
    public User? User { get; set; }
    public int UserId { get; set; }
    public DateTime? Start { get; set; } = System.DateTime.Now;
    public DateTime? End { get; set; } = System.DateTime.Now;
    public string Text { get; set; }
}
