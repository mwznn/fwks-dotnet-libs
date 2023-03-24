﻿using Fwks.Core;
using Fwks.FwksService.Application.Services;
using Fwks.FwksService.Core.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.FwksService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerService, CustomerService>()
            .AddNotificationContext();
    }
}