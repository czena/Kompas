namespace Circles.Domain.Abstractions;

public interface IUserRepository
{
    Task<User?> Get(string login, string password, CancellationToken token);
}