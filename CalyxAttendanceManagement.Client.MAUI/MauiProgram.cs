using CalyxAttendanceManagement.Client.MAUI.Data;
using Microsoft.Extensions.Logging;
using Radzen;

namespace CalyxAttendanceManagement.Client.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            // mudblazor
            builder.Services.AddMudServices();

            // radzen blazor
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            // Local Storage
            builder.Services.AddBlazoredLocalStorage();

            // Authorization
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}