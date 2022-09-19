using System.Threading;
using System.Threading.Tasks;
using Circles.Application.Services.Interfaces;
using Circles.Domain;
using Circles.Domain.Abstractions;

namespace Circles.Application.Services;

public class UserService: IUserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Get(string login, string password, CancellationToken token)
    {
        var user = await _userRepository.Get(login, password, token);
        return user;
    }
}