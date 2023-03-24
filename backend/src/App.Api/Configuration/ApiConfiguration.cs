using FluentValidation;
using FluentValidation.AspNetCore;
using Fwks.AspNetCore.Conventions;
using Fwks.AspNetCore.Extensions;
using Fwks.AspNetCore.Filters.ActionFilters;
using Fwks.AspNetCore.Filters.ExceptionFilters;
using Fwks.AspNetCore.Filters.ResultFilters;
using Fwks.FwksService.App.Api.Configuration.ExceptionHandlers;
using Fwks.FwksService.Core.Configuration;
using Fwks.FwksService.Core.Exceptions;
using Fwks.FwksService.Core.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            .AddExceptionHandlers()
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

                x.AddApplicationNotificationResponses(StatusCodes.Status500InternalServerError, StatusCodes.Status401Unauthorized);

                x.Filters.Add<RequestValidationActionFilter>();
                x.Filters.Add<ExceptionHandlerFilter>();
                x.Filters.Add<NotificationResultFilter>();

                x.Conventions.Add(new SlugCaseRouteTransformerConvention());
            })
            .AddJsonOptions(x => JsonSerializerConfiguration.Configure(x.JsonSerializerOptions))
            .Services;
    }

    private static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        return services
            .AddDefaultExceptionHandlers()
            .AddExceptionHandler<CustomException, CustomExceptionHandler>();
    }
}
