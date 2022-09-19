using System;
using Circles.Persistence.Common;
using FluentMigrator;

namespace Circles.Persistence.Migrations;

[Migration(1, "Initial migration")]
public sealed class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider services)
    {
        return @"CREATE TABLE circles
                (
                    id SERIAL PRIMARY KEY,
                    description text NOT NULL,
                    x bigint NOT NULL,
                    y bigint NOT NULL
                );
                CREATE TABLE users
                (
                    login text PRIMARY KEY,
                    password text
                );
                INSERT INTO circles (description, x, y)
                VALUES
                ('Окружность 1', 0, 10),
                ('Окружность 2', 100, 100),
                ('Окружность 3', 150, 100),
                ('Окружность 4', 33, 89),
                ('Окружность 5', 120, 400),
                ('Окружность 6', 18, 3),
                ('Окружность 7', 2500, 3020)"
            ;
    }

    protected override string GetDownSql(IServiceProvider services)
    {
        return @"DROP TABLE circles;
                DROP TABLE users;";
    }
}