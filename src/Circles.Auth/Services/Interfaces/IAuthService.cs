namespace Circles.Auth.Services.Interfaces;

public interface IAuthService
{
    string GenerateTokenByLogin(string symmetricKey, string userName);
}