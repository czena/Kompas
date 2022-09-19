using Circles.Persistence.Common;

namespace Circles.Api;

public sealed class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString =
            "Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=pwd;No Reset On Close=true; Include Error Detail=true";
        services.AddFluentMigrator(connectionString, typeof(SqlMigration).Assembly);
        services.AddRouting();

    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
        app.UseRouting();
}