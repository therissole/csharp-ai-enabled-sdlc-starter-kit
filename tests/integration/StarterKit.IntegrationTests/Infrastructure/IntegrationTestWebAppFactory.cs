using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using StarterKit.Api.Infrastructure;
using Testcontainers.PostgreSql;

namespace StarterKit.IntegrationTests.Infrastructure;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .WithDatabase("starterkit_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove existing database connection factory
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbConnectionFactory));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add test database connection factory
            services.AddSingleton<IDbConnectionFactory>(
                new NpgsqlConnectionFactory(_dbContainer.GetConnectionString()));
        });

        builder.UseEnvironment("Testing");
    }

    public async Task StartAsync()
    {
        await _dbContainer.StartAsync();
        await ApplyMigrations();
    }

    private async Task ApplyMigrations()
    {
        var connectionString = _dbContainer.GetConnectionString();
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        // Apply migrations from SQL files
        var migration1 = await File.ReadAllTextAsync("../../../../db/migrations/V1__create_languages_table.sql");
        await using var cmd1 = new NpgsqlCommand(migration1, connection);
        await cmd1.ExecuteNonQueryAsync();

        var migration2 = await File.ReadAllTextAsync("../../../../db/migrations/V2__create_greetings_table.sql");
        await using var cmd2 = new NpgsqlCommand(migration2, connection);
        await cmd2.ExecuteNonQueryAsync();
    }

    public async Task StopAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}
