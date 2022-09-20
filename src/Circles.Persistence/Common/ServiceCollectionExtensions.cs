using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Circles.Persistence.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFluentMigrator(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
    {
        var connectionString = configuration["ConnectionString"];
        services
            .AddFluentMigratorCore()
            .ConfigureRunner(
                builder => builder
                    .AddPostgres()
                    .ScanIn(assembly).For.Migrations())
            .AddOptions<ProcessorOptions>()
            .Configure(
                options =>
                {
                    options.ProviderSwitches = "Force Quote=false";
                    options.Timeout = TimeSpan.FromMinutes(10);
                    options.ConnectionString = connectionString;
                });
        return services;
    }
}