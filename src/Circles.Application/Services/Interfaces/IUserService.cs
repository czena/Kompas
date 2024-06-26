﻿using Circles.Domain;

namespace Circles.Application.Services.Interfaces;

public interface IUserService
{
    Task<User?> Get(string login, string password, string salt, CancellationToken ct);
}