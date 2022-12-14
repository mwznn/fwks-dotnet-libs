using Fwks.FwksService.Application;
using Fwks.FwksService.Infra.Mongo;
using Fwks.FwksService.Core.Settings;
using Fwks.FwksService.Infra.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.FwksService.App.Api.Configuration;

internal static class DependenciesConfiguration
{
    internal static IServiceCollection AddDependencies(this IServiceCollection services, AppSettings appSettings)
    {
        return services
            .AddMediatorServices(appSettings)
            .AddApplicationServices()
            .AddMongoDb(appSettings)
            .AddPostgres(appSettings);
    }
}