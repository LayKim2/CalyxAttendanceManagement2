using CalyxAttendanceManagement.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// mudblazor
builder.Services.AddMudServices();

// radzen blazor
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//DI
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IPTOService, PTOService>();
builder.Services.AddScoped<IOpenAIService, OpenAIService>();


// Local Storage
builder.Services.AddBlazoredLocalStorage();

// Authorization
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();