using System.IO.Compression;
using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.AspNetCore.Extensions;

public static class ApiConfigurationExtensions
{
    public static IServiceCollection AddDefaultVersioning(this IServiceCollection services)
    {
        return services
            .AddApiVersioning(x =>
            {
                x.ReportApiVersions = true;
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.DefaultApiVersion = new ApiVersion(1, 0);
            })
            .AddApiExplorer(x =>
            {
                x.GroupNameFormat = "'v'V";
                x.SubstituteApiVersionInUrl = true;
            })
            .Services;
    }

    public static IServiceCollection AddDefaultResponseCompression(this IServiceCollection services)
    {
        return services
            .Configure<GzipCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal)
            .AddResponseCompression(x =>
            {
                x.EnableForHttps = true;
                x.Providers.Add<GzipCompressionProvider>();
            });
    }
}
