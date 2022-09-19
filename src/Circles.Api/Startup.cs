using Circles.Application.Services;
using Circles.Application.Services.Interfaces;
using Circles.Auth.Common;
using Circles.Auth.Services;
using Circles.Auth.Services.Interfaces;
using Circles.Domain.Abstractions;
using Circles.Persistence;
using Circles.Persistence.Common;
using Circles.Persistence.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Circles.Api;

public sealed class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = _configuration.GetSection(nameof(ConnectionStringConfiguration));
        services.Configure<ConnectionStringConfiguration>(connectionString);

        services.AddControllers();
        services.AddFluentMigrator(connectionString.Get<ConnectionStringConfiguration>(), typeof(SqlMigration).Assembly);
        services.AddAuth(_configuration);
        services.AddRouting();

        
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IAuthService, AuthService>();

        services.AddSwaggerGen();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            opt.RoutePrefix = string.Empty;
        });
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}