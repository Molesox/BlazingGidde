using System.Text;
using BlazingGidde.Server.Data;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Server.Services;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;
using BlazingGidde.Shared.Models.Patois;
using BlazingGidde.Shared.Models.PersonMain;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

builder.Services.AddTransient<IRepository<Person>, RepositoryEF<Person, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<Email>, RepositoryEF<Email, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<Phone>, RepositoryEF<Phone, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<DictionaryEntry>, RepositoryEF<DictionaryEntry, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<CustomTemplateItem>,RepositoryEF<CustomTemplateItem, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<Incidency>,RepositoryEF<Incidency, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<Template>, RepositoryEF<Template, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<TemplateItem>, RepositoryEF<TemplateItem, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<TemplateKind>, RepositoryEF<TemplateKind, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<TemplateType>, RepositoryEF<TemplateType, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<BreakeableItem>, RepositoryEF<BreakeableItem, ApplicationDbContext>>();
builder.Services.AddTransient<IRepository<GazItem>, RepositoryEF<GazItem, ApplicationDbContext>>();
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
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

    setup.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Flow",
        Description = "Flow API",
    });

});


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