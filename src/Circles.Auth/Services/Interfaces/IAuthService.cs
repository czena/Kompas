namespace Circles.Auth.Services.Interfaces;

public interface IAuthService
{
    object GenerateTokenByLogin(string symmetricKey, string userName);
}