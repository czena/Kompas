using System.Data;
using Circles.Domain;
using Circles.Domain.Abstractions;
using Circles.Persistence.Configurations;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Circles.Persistence;

public class UserRepository: IUserRepository
{
    private readonly string _connectionString;
    private string t = "";
    public UserRepository(IOptions<ConnectionStringConfiguration> connectionStringOptions)
    {
        _connectionString = connectionStringOptions.Value.ConnectionString ?? "";
    }

    public async Task<User?> Get(string login, string password, CancellationToken token)
    {
        try
        {
            if (login.Length == 0) return null;
            const string query = @"
                                SELECT login,
                                       password
                                FROM users
                                WHERE login = :login AND password = :password;
                                ";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection)
            {
                Parameters =
                {
                    new NpgsqlParameter<string>("login", login),
                    new NpgsqlParameter<string>("password", password),
                }
            };
            await connection.OpenAsync(token);
            await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess, token);
            while (await reader.ReadAsync(token))
            {
                var pwd = reader.GetFieldValue<string>(1);
                return new User(login, pwd);
            }
        }
        catch (Exception)
        {
            return null;
        }
        return null;
    }
}