using System;
using Microsoft.AspNetCore.Builder;
using Fwks.AspNetCore.Middlewares.BuildInfo.Models;

namespace Fwks.AspNetCore.Middlewares.BuildInfo.Extensions;

public static class BuildVersionExtensions
{
    public static IApplicationBuilder UseBuildInfoEndpoint(this IApplicationBuilder appBuilder, Action<BuildInfoEndpointOptions> optionsAction = default)
    {
        var options = new BuildInfoEndpointOptions();

        optionsAction?.Invoke(options);

        return appBuilder
            .MapWhen(
                context => context.Request.Path.Equals(options.Route, StringComparison.InvariantCultureIgnoreCase),
                app => app.UseMiddleware<BuildInfoEndpointMiddleware>(options));
    }
}