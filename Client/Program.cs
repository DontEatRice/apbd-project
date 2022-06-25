using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazor_project.Client;
using Microsoft.AspNetCore.Components.Authorization;
using blazor_project.Client.Service;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjYwMjIyQDMyMzAyZTMxMmUzMGt3Ny9VOWJvbmdhTk9ROWtwNmVldFl4VGRuQVk4dzQxQUNnYnNYcVdoTFU9");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSyncfusionBlazor(options => { });

await builder.Build().RunAsync();
