using Circles.Api.DTOs;
using Circles.Domain;

namespace Circles.Api.Mappers;

public static class UserMapper
{
    public static User ToDomain(this UserDTO user)
    {
        return new User(user.Login, user.Password);
    }
}