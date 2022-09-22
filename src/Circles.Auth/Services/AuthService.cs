using Circles.Auth.Common;
using Circles.Auth.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Circles.Auth.Services;

public class AuthService: IAuthService
{
    private IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateTokenByLogin(string symmetricKey, string userName)
    {
        return TokenHelper.GenerateToken(symmetricKey, userName, _configuration);
    }
}