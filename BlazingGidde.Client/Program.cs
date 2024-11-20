using BlazingGidde.Client;
using BlazingGidde.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU3OTEyMEAzMjM3MmUzMDJlMzBYdGxweHFSTXJWVkVhMkZ2Vm11WkJCcERmZkEyYXd6YUgwYW5WTUtSUGVvPQ==");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<AspNetUserManager>();
builder.Services.AddScoped<AspNetRoleManager>();

builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();