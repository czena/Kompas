using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Circles.Auth.Common;

internal static class TokenHelper
{
    public static string GenerateToken(string symmetricKey, string userName, IConfiguration configuration) 
    {
        var signingCredentials = new SigningCredentials(
            key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricKey)),
            algorithm: SecurityAlgorithms.HmacSha256);
        var jwtDate = DateTime.Now;

        var jwt = new JwtSecurityToken(
            audience: configuration["Jwt:Audience"],
            issuer: configuration["Jwt:Issuer"],
            claims: new List<Claim> {new(ClaimTypes.NameIdentifier, userName)},
            notBefore: jwtDate,
            expires: jwtDate.AddSeconds(30),
            signingCredentials: signingCredentials
        );
        
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }
}