using System;
using Fwks.AspNetCore.Abstractions.ErrorHandlers;
using Fwks.AspNetCore.ErrorHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.AspNetCore.Extensions;

public static class ExceptionConfigurationExtensions
{
    public static IServiceCollection AddDefaultExceptionHandlers(this IServiceCollection services)
    {
        return services
            .AddSingleton<IExceptionHandler<Exception>, UnexpectedErrorExceptionHandler>();
    }

    public static IServiceCollection AddExceptionHandler<TException, TExceptionHandler>(this IServiceCollection services)
        where TException : Exception
        where TExceptionHandler : ExceptionHandler<TException>
    {
        return services
            .AddSingleton<IExceptionHandler<Exception>, TExceptionHandler>();
    }
}