using System;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Fwks.Core.Abstractions.Builders;
using Fwks.Redis.Options;
using Fwks.Redis.Abstractions.Connectors;
using Fwks.Redis.Abstractions.Services;
using Fwks.Redis.Connectors;
using Fwks.Redis.Services;

namespace Fwks.Redis;

public static class ConfigureServices
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConnectionStringBuilder builder, Action<RedisServiceOptions> optionsAction = default)
    {
        RedisServiceOptions options = new(false, false);

        optionsAction?.Invoke(options);

        return services
            .AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.BuildConnectionString()))
            .AddSingleton(options)
            .AddSingleton<IRedisConnection, RedisConnection>()
            .AddScoped<IRedisService, RedisService>();
    }
}
