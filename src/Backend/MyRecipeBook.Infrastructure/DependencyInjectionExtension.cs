﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enums;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseType = configuration.GetConnectionString("DatabaseType");

            var databaseTypeEnum = (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseType!);

            if (databaseTypeEnum == DatabaseType.MySql)
                AddDbContext_MySql(services, configuration);

            if (databaseTypeEnum == DatabaseType.SqlServer)
                AddDbContext_SqlServer(services, configuration);

            AddRepositories(services);
        }

        private static void AddDbContext_MySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionMySQL");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

            services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
        }

        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionSQLServer");

            services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
