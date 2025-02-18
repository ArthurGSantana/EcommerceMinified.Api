using System;
using EcommerceMinified.Data.Postgres.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceMinified.IoC;

public class DependencyContainer

{
    public static void RegisterServices(IServiceCollection services, string postgresConnectionString)
    {
        services.AddDbContext<PostgresDbContext>(options =>
            {
                if (!string.IsNullOrEmpty(postgresConnectionString))
                {
                    options.UseNpgsql(postgresConnectionString);
                }
            }
        );

    }
}
