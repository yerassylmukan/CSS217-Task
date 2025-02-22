using System.Text.Json.Serialization;
using Identity;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServiceCollection(builder.Configuration);
builder.Services.AddPersistenceServiceCollection(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddLogging();

var app = builder.Build();

app.Run();