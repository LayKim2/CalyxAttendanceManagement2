
using System.ComponentModel.DataAnnotations.Schema;

namespace CalyxAttendanceManagement.Shared.Model;

public class UserPTOHistory
{
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public int UserPTOId { get; set; }
    public string PTOType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal NeedPTO { get; set; }
    public decimal CurrentPTO { get; set; }
    public decimal CalculatedPTO { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string VerifiedType { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? VerifiedDate { get; set; }

    [NotMapped]
    public string Date { get; set; } = string.Empty;
}
