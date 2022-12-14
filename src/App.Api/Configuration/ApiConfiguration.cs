using FluentValidation;
using FluentValidation.AspNetCore;
using Fwks.AspNetCore.Extensions;
using Fwks.AspNetCore.Filters.ActionFilters;
using Fwks.AspNetCore.Filters.ResultFilters;
using Fwks.AspNetCore.Transformers;
using Fwks.FwksService.Core.Domain.Responses;
using Fwks.FwksService.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.FwksService.App.Api.Configuration;

internal static class ApiConfiguration
{
    internal static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        return services
            .AddHttpClient()
            .AddDefaultResponseCompression()
            .AddDefaultVersioning()
            .AddFluentValidation()
            .AddControllersConfiguration();
    }

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        return services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(typeof(CustomerResponse).Assembly)
            .Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);
    }

    private static IServiceCollection AddControllersConfiguration(this IServiceCollection services)
    {
        return services
            .AddControllers(x =>
            {
                x.AddJsonAsDefaultMediaType();

                x.AddApplicationNotificationResponse(StatusCodes.Status500InternalServerError, StatusCodes.Status401Unauthorized);

                x.Filters.Add<NotificationResultFilter>();
                x.Filters.Add<FluentValidationModelStateFilter>();

                x.Conventions.Add(new RouteTokenTransformerConvention(new SlugCaseParameterTransformer()));
            })
            .AddJsonOptions(x => JsonSerializerConfiguration.Configure(x.JsonSerializerOptions))
            .Services;
    }
}
