using System.Text;
using System.Text.Json.Serialization;
using AgileObjects.AgileMapper;
using BlazingGidde.Server.Data;
using BlazingGidde.Server.Data.Repository;
using BlazingGidde.Server.Services;
using BlazingGidde.Shared.Models.FlowCheck;
using BlazingGidde.Shared.Models.FlowCheck.TemplateItems;
using BlazingGidde.Shared.Models.Patois;
using BlazingGidde.Shared.Models.PersonMain;
using BlazingGidde.Shared.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Templates;
using Serilog.Templates.Themes;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

Mapper.WhenMapping.MapEntityKeys();

// Add services to the container.
// https://learn.microsoft.com/fr-fr/aspnet/core/web-api/?view=aspnetcore-9.0
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true)
    .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddRazorPages();

builder.Services.AddSerilog((services, lc) => lc
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console(new ExpressionTemplate(
        "[{@t:HH:mm:ss} {@l:u3}{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end}] {@m}\n{@x}",
        theme: TemplateTheme.Code)));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<FlowUser>()
    .AddRoles<FlowRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var entityTypes = new[]
{
    typeof(Person),
    typeof(Email),
    typeof(Phone),
    typeof(DictionaryEntry),
    typeof(CustomTemplateItem),
    typeof(Incidency),
    typeof(Template),
    typeof(TemplateItem),
    typeof(TemplateKind),
    // typeof(TemplateType), // Types with their own repositories
    typeof(BreakeableItem),
    typeof(GazItem)
};

// Add repositories for standard entities
foreach (var entityType in entityTypes)
{
    var repositoryInterface = typeof(IRepository<>).MakeGenericType(entityType);
    var repositoryImplementation =
        typeof(RepositoryEF<,,>).MakeGenericType(entityType, typeof(ApplicationDbContext), typeof(int));
    builder.Services.AddTransient(repositoryInterface, repositoryImplementation);
}

// Handle FlowUser and FlowRole as string-based repositories
var stringBasedEntityTypes = new[]
{
    typeof(FlowUser),
    typeof(FlowRole)
};

foreach (var entityType in stringBasedEntityTypes)
{
    var repositoryInterface = typeof(IRepository<>).MakeGenericType(entityType);
    var repositoryImplementation =
        typeof(RepositoryEF<,,>).MakeGenericType(entityType, typeof(ApplicationDbContext), typeof(string));
    builder.Services.AddTransient(repositoryInterface, repositoryImplementation);
}

builder.Services.AddTransient<ITypeRepositoryEF<TemplateType>, TypeRepositoryEF<TemplateType, ApplicationDbContext>>();
builder.Services.AddTransient<IUserRepository<FlowUser>, RepositoryUser>();
builder.Services.AddTransient<IRoleRepository<FlowRole>, RepositoryRole>();
builder.Services.AddTransient<UserRoleService>();

builder.Services.AddSingleton<TemplateRenderer>();

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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]!))
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
        Description = "Flow API"
    });
});

var app = builder.Build();

app.UseSerilogRequestLogging(options =>
{
    // Customize the message template
    options.MessageTemplate = "Handled {RequestPath}";

    // Emit debug-level events instead of the defaults
    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

    // Attach additional properties to the request completion event
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
    };
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
    app.UseDeveloperExceptionPage();
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