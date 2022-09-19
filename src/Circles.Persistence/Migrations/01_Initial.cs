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
                );";
    }

    protected override string GetDownSql(IServiceProvider services)
    {
        return @"DROP TABLE circles;
                DROP TABLE users;";
    }
}