using System;
using Fwks.FwksService.Core.Domain;
using Fwks.FwksService.Core.Settings;
using Fwks.FwksService.Core.Abstractions.Repositories;
using Fwks.FwksService.Infra.Mongo.Abstractions;
using Fwks.FwksService.Infra.Mongo.Repositories;
using Fwks.MongoDb;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.FwksService.Infra.Mongo;

public static class ConfigureServices
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, AppSettings appSettings)
    {
        return services
             .AddMongoDb<IEntityMap>(appSettings.Storage.MongoDb, appSettings.Storage.MongoDb.Database)
             .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerRepository<CustomerDocument, Guid>, CustomerRepository>();
    }
}