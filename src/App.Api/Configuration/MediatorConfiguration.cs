using Fwks.FwksService.Core.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.FwksService.App.Api.Configuration;

internal static class MediatorConfiguration
{
    internal static IServiceCollection AddMediatorServices(this IServiceCollection services, AppSettings appSettings)
    {
        return services
            .AddMediator(x =>
            {
                x.Namespace = "Fwks.FwksService.App.Api.Mediator";
                x.ServiceLifetime = ServiceLifetime.Scoped;
            });
    }
}