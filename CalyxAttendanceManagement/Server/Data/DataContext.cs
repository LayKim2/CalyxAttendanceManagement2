namespace CalyxAttendanceManagement.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserPTO> UserPTO { get; set; }
    public DbSet<UserPTOHistory> UserPTOHistory { get; set; }
    public DbSet<Calendar> Calendar { get; set; }
}
