using System.Text.Json.Serialization;
using Identity;
using Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Data;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServiceCollection(builder.Configuration);
builder.Services.AddIdentityServiceCollection(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.EnableAnnotations();
    c.SchemaFilter<CustomSchemaFilters>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


builder.Services.AddLogging();

var app = builder.Build();

app.Logger.LogInformation("Web API created...");

app.Logger.LogInformation("Seeding Database...");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var appDbContext = services.GetRequiredService<ApplicationDbContext>();
    var appIdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
    appDbContext.Database.Migrate();
    appIdentityDbContext.Database.Migrate();
}
catch (Exception e)
{
    app.Logger.LogError(e, "An error occurred while seeding the database.");
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program
{
}