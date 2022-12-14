using Fwks.Core.Abstractions.Builders;
using Fwks.Redis.Abstractions.Connectors;
using Fwks.Redis.Abstractions.Services;
using Fwks.Redis.Connectors;
using Fwks.Redis.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Fwks.Redis;

public static class ConfigureServices
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConnectionStringBuilder builder)
    {
        return services
            .AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.BuildConnectionString()))
            //.AddSingleton<RedisServiceOptions>
            .AddSingleton<IRedisConnection, RedisConnection>()
            .AddScoped<IRedisService, RedisService>();
    }

    public static IServiceCollection AddRedis2(this IServiceCollection services, IConnectionStringBuilder builder)
    {
        return services
            .AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.BuildConnectionString()))
            .AddSingleton<IRedisConnection, RedisConnection>()
            .AddScoped<IRedisService, RedisService>();
    }
}
