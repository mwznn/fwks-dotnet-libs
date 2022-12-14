using Fwks.AspNetCore.Middlewares.BuildInfo.Extensions;
using Fwks.FwksService.App.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Fwks.FwksService.App.Api.Configuration;

internal static class MiddlewareConfiguration
{
    internal static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        return app
            .UseBuildInfoEndpoint()
            .UseMiddleware<ErrorHandlerMiddleware>();
    }
}