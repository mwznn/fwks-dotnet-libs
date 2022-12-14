using Fwks.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.FwksService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddNotificationContext();
    }
}