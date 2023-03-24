using Fwks.AspNetCore.Middlewares.BuildInfo.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Fwks.FwksService.App.Api.Configuration;

internal static class MiddlewareConfiguration
{
    internal static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        return app
            .UseBuildInfoEndpoint();
    }
}