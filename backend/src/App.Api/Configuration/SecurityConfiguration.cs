using Fwks.AspNetCore.Extensions;
using Fwks.Core.Extensions;
using Fwks.FwksService.Core.Settings;
using Fwks.Security.Obfuscation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace Fwks.FwksService.App.Api.Configuration;

internal static class SecurityConfiguration
{
    internal static IServiceCollection AddSecurity(this IServiceCollection services, AppSettings appSettings)
    {
        Obfuscator.Setup(appSettings.Security.ObfuscationKey, 7);

        return services
            .AddAuthServer(appSettings)
            .AddDefaultCors(x =>
            {
                x.AllowedOrigins = appSettings.Security.Cors.AllowedOrigins;
                x.AllowedHeaders = appSettings.Security.Cors.AllowedHeaders;
            });
    }

    internal static IApplicationBuilder UseSecurity(this IApplicationBuilder app)
    {
        return app
            .UseDefaultCors()
            .UseAuthentication()
            .UseAuthorization();
    }

    private static IServiceCollection AddAuthServer(this IServiceCollection services, AppSettings appSettings)
    {
        if (appSettings.Security.AuthServer.Authority.IsEmpty())
            return services;

        IdentityModelEventSource.HeaderWritten = false;

        return services
            .AddAuthorizationCore()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.Authority = appSettings.Security.AuthServer.Authority;
                x.Audience = appSettings.Security.AuthServer.Audience;
                x.RequireHttpsMetadata = appSettings.Security.AuthServer.RequireHttpsMetadata;
                x.Events = new JwtBearerEvents { OnChallenge = JwtBearerEventsExtensions.OnChallenge() };
            })
            .Services;
    }
}