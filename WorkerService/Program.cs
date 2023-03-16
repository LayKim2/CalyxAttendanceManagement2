using Microsoft.EntityFrameworkCore;
using WorkerService;
using WorkerService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        AppSettings.Configuration = configuration;
        AppSettings.ConnectionString = configuration.GetConnectionString("DefaultConnection");

        var optionBuilder = new DbContextOptionsBuilder<DataContext>();
        optionBuilder.UseSqlServer(AppSettings.ConnectionString);

        services.AddScoped<DataContext>(d => new DataContext(optionBuilder.Options));

        services.AddHostedService<Worker>();
    })
    .Build();


await host.RunAsync();
