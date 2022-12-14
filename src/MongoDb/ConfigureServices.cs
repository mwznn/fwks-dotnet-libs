using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Fwks.Core.Abstractions.Builders;

namespace Fwks.MongoDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb<TMapAssembly>(this IServiceCollection services, IConnectionStringBuilder builder, string database)
    {
        RegisterEntityMappers();

        var mongoClient = new MongoClient(builder.BuildConnectionString());

        return services
            .AddSingleton<IMongoClient>(mongoClient)
            .AddSingleton(mongoClient.GetDatabase(database));

        static void RegisterEntityMappers()
        {
            _ = typeof(TMapAssembly).Assembly.GetTypes()
                .Where(x => x.IsClass && x.IsAssignableTo(typeof(TMapAssembly)))
                .Select(x => Activator.CreateInstance(x))
                .ToList();
        }
    }
}