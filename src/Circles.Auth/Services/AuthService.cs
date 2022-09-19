using Circles.Auth.Common;
using Circles.Auth.Services.Interfaces;

namespace Circles.Auth.Services;

public class AuthService: IAuthService
{
    public object GenerateTokenByLogin(string symmetricKey, string userName)
    {
        return TokenHelper.GenerateToken(symmetricKey, userName);
    }
}