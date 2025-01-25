using AgileObjects.AgileMapper;
using BlazingGidde.Client;
using BlazingGidde.Client.Services;
using BlazingGidde.Client.Services.FlowCheck;
using BlazingGidde.Client.Services.FlowCheck.TemplateItems;
using BlazingGidde.Shared.DTOs.FlowCheck;
using Blazored.LocalStorage;
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddDevExpressBlazor(configure => configure.BootstrapVersion = BootstrapVersion.v5);

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ThemeService>();

builder.Services.AddScoped<AspNetUserManager>();
builder.Services.AddScoped<AspNetRoleManager>();
builder.Services.AddScoped<UserRoleManager>();
builder.Services.AddScoped<PersonMainManager>();
builder.Services.AddScoped<DictionaryManager>();

builder.Services.AddScoped<BreakeableItemManager>();
builder.Services.AddScoped<GazItemManager>();
builder.Services.AddScoped<CustomTemplateItemManager>();
builder.Services.AddScoped<IncidencyManager>();
builder.Services.AddScoped<TemplateManager>();
builder.Services.AddScoped<TemplateItemManager>();
builder.Services.AddScoped<TemplateKindManager>();
builder.Services.AddScoped<TemplateTypeManager>();

Mapper.WhenMapping
    .From<string>()
    .To<NameFlowRoleDto>()
    .Map(ctx => ctx.Source)
    .To(dto => dto.Name);

await builder.Build().RunAsync();