using Fwks.FwksService.Core.Domain;
using Fwks.FwksService.Core.Settings;
using Fwks.FwksService.Core.Abstractions.Repositories;
using Fwks.FwksService.Infra.Postgres.Contexts;
using Fwks.FwksService.Infra.Postgres.Repositories;
using Fwks.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.FwksService.Infra.Postgres;

public static class ConfigureServices
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, AppSettings appSettings)
    {
        return services
            .AddPostgres<AppServiceContext>(appSettings.Storage.Postgres)
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerRepository<Customer, int>, CustomerRepository>();
    }
}