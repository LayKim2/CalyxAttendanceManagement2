using CalyxAttendanceManagement.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Services;

public class DbHelper
{
    private DataContext _context;

    private DbContextOptions<DataContext> GetAllOptions()
    {
        var optionBuilder = new DbContextOptionsBuilder<DataContext>();

        optionBuilder.UseSqlServer(AppSettings.ConnectionString);

        return optionBuilder.Options;
    }

    public void UpdatePTO()
    {
        try
        {
            using (_context = new DataContext(GetAllOptions()))
            {
                _context.Database.ExecuteSqlRaw("UpdatePTOByStartWorkDate");
            }
        }
        catch (Exception e)
        {
            
        }

    }
}
