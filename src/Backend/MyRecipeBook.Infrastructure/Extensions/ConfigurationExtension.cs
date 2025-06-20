﻿using Microsoft.Extensions.Configuration;
using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Infrastructure.Extensions;
public static class ConfigurationExtension
{
    public static bool IsUnitTestEnviroment(this IConfiguration configuration) => configuration.GetValue<bool>("InMemoryTest");

    public static DatabaseType DatabaseType(this IConfiguration configuration)
    {
        var databaseType = configuration.GetConnectionString("DatabaseType");

        return (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseType!);
    }

    public static string ConnectionString(this IConfiguration configuration)
    {
        var databaseType = configuration.DatabaseType();

        if (databaseType == Domain.Enums.DatabaseType.MySql)
            return configuration.GetConnectionString("ConnectionMySql")!;

        if (databaseType == Domain.Enums.DatabaseType.SqlServer)
            return configuration.GetConnectionString("ConnectionSqlServer")!;

        return "There isn't a connection to a known databases";
    }
}
