using Circles.Application.Common;
using Circles.Application.Services.Interfaces;
using Circles.Domain;
using Circles.Domain.Abstractions;

namespace Circles.Application.Services;

public sealed class UserService: IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Get(string login, string password, string salt, CancellationToken token)
    {
        var hashedPassword = HashHelper.GetHash(HashHelper.GetHash(password) + salt);
        var user = await _userRepository.Get(login, hashedPassword, token);
        return user;
    }
}