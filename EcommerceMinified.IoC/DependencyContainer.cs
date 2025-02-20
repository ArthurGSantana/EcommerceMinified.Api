using EcommerceMinified.Data.Postgres.Context;
using EcommerceMinified.Data.Repository;
using EcommerceMinified.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceMinified.IoC;

public class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services, string postgresConnectionString)
    {
        #region Postgres
        services.AddDbContext<PostgresDbContext>(options =>
            {
                if (!string.IsNullOrEmpty(postgresConnectionString))
                {
                    options.UseNpgsql(postgresConnectionString);
                }
            }
        );
        #endregion

        #region UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion
    }
}
