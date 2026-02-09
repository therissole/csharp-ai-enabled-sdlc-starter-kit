using Microsoft.FeatureManagement;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using StarterKit.Api.Features.Greetings;
using StarterKit.Api.Features.Health;
using StarterKit.Api.Features.Languages;
using StarterKit.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddOpenApi();

// Add Feature Management
builder.Services.AddFeatureManagement();

// Add OpenTelemetry
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("StarterKit.Api"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter();
    });

// Add Database Connection Factory
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Host=localhost;Database=starterkit;Username=postgres;Password=postgres";
builder.Services.AddSingleton<IDbConnectionFactory>(new NpgsqlConnectionFactory(connectionString));

// Add repositories
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IGreetingRepository, GreetingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map feature endpoints using vertical slice architecture
app.MapGroup("/api/health")
    .MapHealthEndpoints();

app.MapGroup("/api/languages")
    .MapLanguageEndpoints();

app.MapGroup("/api/greetings")
    .MapGreetingEndpoints();

app.Run();

// Make Program class accessible to tests
public partial class Program { }

