using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Circles.Auth.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Symmetric:Key"]));
                options.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = symmetricKey,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true, 
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ClockSkew = TimeSpan.FromSeconds(30)
                };
            });
        return services;
    }
}