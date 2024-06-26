﻿using Circles.Application.Services;
using Circles.Application.Services.Interfaces;
using Circles.Auth.Common;
using Circles.Auth.Services;
using Circles.Auth.Services.Interfaces;
using Circles.Domain.Abstractions;
using Circles.Persistence;
using Circles.Persistence.Common;

namespace Circles.Api;

public sealed class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
        services.AddFluentMigrator(_configuration, typeof(SqlMigration).Assembly);
        services.AddAuth(_configuration);
        services.AddRouting();
        
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<ICircleRepository, CircleRepository>();
        
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<ICircleService, CircleService>();
        services.AddSingleton<IAuthService, AuthService>();

        services.AddSwaggerGen();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            opt.RoutePrefix = string.Empty;
        });
        
        app.UseRouting();
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}