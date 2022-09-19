using System.Threading;
using System.Threading.Tasks;

namespace Circles.Domain.Abstractions;

public interface IUserRepository
{
    Task<User?> Get(string login, string password, CancellationToken token);
}