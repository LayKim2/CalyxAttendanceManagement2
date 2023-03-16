using CalyxAttendanceManagement.Server.Services.AuthService;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerService.Services;

public class Worker : BackgroundService
{
    private Timer _timer;
    private DbHelper _dbHelper;

    public Worker()
    {
        _dbHelper = new DbHelper();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // 매일 오전 8시에 작업을 수행하는 Timer 설정
        var now = DateTime.Now;
        var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 08, 00, 00, DateTimeKind.Local);
        if (now > scheduledTime)
        {
            scheduledTime = scheduledTime.AddDays(1);
        }
        var dueTime = scheduledTime - now;
        _timer = new Timer(DoWork, null, dueTime, TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        _dbHelper.UpdatePTO();
    }

    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}