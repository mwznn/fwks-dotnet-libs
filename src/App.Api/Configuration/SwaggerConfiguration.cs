using Fwks.AspNetCore.Extensions;
using Fwks.AspNetCore.Filters.OperationFilters;
using Fwks.FwksService.Core.Settings;
using Fwks.FwksService.App.Api.Configuration.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fwks.FwksService.App.Api.Configuration;

internal static class SwaggerConfiguration
{
    internal static IServiceCollection AddSwagger(this IServiceCollection services, AppSettings appSettings)
    {
        if (appSettings.IsProduction())
            return services;

        return services
            .AddSingleton<IConfigureOptions<SwaggerGenOptions>, SwaggerVersionConfiguration>()
            .AddSwaggerGen(x =>
            {
                x.DescribeAllParametersInCamelCase();

                x.OperationFilter<SwaggerParametersOperationFilter>();
                x.OperationFilter<JsonIgnorePropertyFilter>();

                x.AddBearerSecurity();

                x.AddOAuth2Security(s =>
                {
                    s.Authority = appSettings.Security.AuthServer.Authority;
                    s.Scopes = appSettings.Security.AuthServer.Scopes;
                });
            });
    }

    internal static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider apiProvider, AppSettings appSettings)
    {
        if (appSettings.IsProduction())
            return app;

        app.UseDefaultSwaggerUI(apiProvider, "Fwks API Swagger");

        return app;
    }
}