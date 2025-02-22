using System.Text.Json.Serialization;
using Identity;
using Identity.Data;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServiceCollection(builder.Configuration);
builder.Services.AddIdentityServiceCollection(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddLogging();

var app = builder.Build();

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