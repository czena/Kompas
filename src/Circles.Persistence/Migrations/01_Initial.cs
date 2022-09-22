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
                CREATE INDEX idx_id ON circles
                (
                    id
                );
                CREATE INDEX idx_login_id ON users
                (
                    login
                );
                INSERT INTO circles (description, x, y)
                VALUES
                ('Окружность 1', 0, 10),
                ('Окружность 2', 100, 100),
                ('Окружность 3', 150, 100),
                ('Окружность 4', 33, 89),
                ('Окружность 5', 120, 400),
                ('Окружность 6', 18, 3),
                ('Окружность 7', 2500, 3020);
                INSERT INTO users (login, password)
                VALUES
                ('admin', 'E37C0A61C2ABBFE419367F572BA7AA11'),
                ('1', 'C372F08B3D96DCBD80576B24E8B03BB9')"
            ;
        
        
    }

    protected override string GetDownSql(IServiceProvider services)
    {
        return @"DROP TABLE circles;
                DROP TABLE users;";
    }
}