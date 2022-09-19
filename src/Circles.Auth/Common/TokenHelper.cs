using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Circles.Auth.Common;

internal static class TokenHelper
{
    public static object GenerateToken(string symmetricKey, string userName) 
    {
        var signingCredentials = new SigningCredentials(
            key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricKey)),
            algorithm: SecurityAlgorithms.HmacSha256);
        var jwtDate = DateTime.Now;

        var jwt = new JwtSecurityToken(
            audience: "circle",
            issuer: "circle",
            claims: new List<Claim> {new(ClaimTypes.NameIdentifier, userName)},
            notBefore: jwtDate,
            expires: jwtDate.AddSeconds(60),
            signingCredentials: signingCredentials
        );
        
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return new {
            jwt = token,
            unixTimeExpiresAt = new DateTimeOffset(jwtDate).ToUnixTimeMilliseconds()
        };
    }
}