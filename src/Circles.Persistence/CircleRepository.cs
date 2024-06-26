﻿using System.Data;
using System.Runtime.CompilerServices;
using Circles.Domain;
using Circles.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Circles.Persistence;

public sealed class CircleRepository: ICircleRepository
{
    private readonly string _connectionString;
    
    public CircleRepository(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionString"];
    }
    
    public async IAsyncEnumerable<Circle> Get([EnumeratorCancellation] CancellationToken ct)
    {
        const string query = @"SELECT id,
                                       x,
                                       y
                                FROM circles
                                ORDER BY id;
                                ";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        await using var command = new NpgsqlCommand(query, connection);
        await connection.OpenAsync(ct);
        await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess, ct);
        while (await reader.ReadAsync(ct))
        {
            var circle = new Circle(
                reader.GetFieldValue<int>(0),
                reader.GetFieldValue<long>(1),
                reader.GetFieldValue<long>(2));
            yield return circle;
        }
    }

    public async Task<int> SetDescription(int id, string description, CancellationToken ct)
    {
        const string query = @"UPDATE circles
                                SET description = :description
                                WHERE id = :id;
                                ";
        await using var connection = new NpgsqlConnection(_connectionString);
        await using var command = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter<string>("description", description),
                new NpgsqlParameter<int>("id", id),
            }
        };
        await connection.OpenAsync(ct);
        return await command.ExecuteNonQueryAsync(ct);
    }

    public async Task<string> GetDescription(int id, CancellationToken ct)
    {
        const string query = @"SELECT description
                                FROM circles
                                WHERE id = :id;
                                ";
        
        await using var connection = new NpgsqlConnection(_connectionString);
        await using var command = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter<int>("id", id),
            }
        };
        await connection.OpenAsync(ct);
        await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess, ct);
        await reader.ReadAsync(ct);
        return reader.GetFieldValue<string>(0);
    }
}