using System;
using Fwks.AspNetCore.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.AspNetCore.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddDefaultCors(this IServiceCollection services, Action<CorsOptions> optionsAction = default)
    {
        CorsOptions options = new();

        optionsAction?.Invoke(options);

        return services
            .AddCors(x => x
                .AddPolicy(options.PolicyName, policy => policy
                    .WithOrigins(options.AllowedOrigins)
                    .WithHeaders(options.AllowedHeaders)
                    .WithMethods(options.AllowedMethods)
                    .WithExposedHeaders(options.AllowedHeaders)));
    }

    public static IApplicationBuilder UseDefaultCors(this IApplicationBuilder app, string policyName = CorsOptions.DEFAULT_POLICY_NAME)
    {
        return app
            .UseCors(policyName);
    }
}