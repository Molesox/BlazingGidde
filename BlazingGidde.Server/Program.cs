using System.Text;
using BlazingGidde.Server.Data;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Server.Services;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;
using BlazingGidde.Shared.Models.Patois;
using BlazingGidde.Shared.Models.PersonMain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<FlowUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddTransient<RepositoryEF<Person, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<Email, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<Phone, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<DictionaryEntry, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<CustomTemplateItem, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<Incidency, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<Template, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<TemplateItem, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<TemplateKind, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<TemplateType, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<BreakeableItem, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryEF<GazItem, ApplicationDbContext>>();
builder.Services.AddTransient<RepositoryUser>();
builder.Services.AddTransient<RepositoryRole>();
builder.Services.AddTransient<UserRoleService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = configuration["JwtAudience"],
        ValidIssuer = configuration["JwtIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]!)),
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();