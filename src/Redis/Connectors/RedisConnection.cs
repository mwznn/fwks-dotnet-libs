using Fwks.Redis.Abstractions.Connectors;
using StackExchange.Redis;

namespace Fwks.Redis.Connectors;

public sealed class RedisConnection : IRedisConnection
{
    public RedisConnection(
        IConnectionMultiplexer multiplexer)
    {
        Database = multiplexer.GetDatabase();

        Subscriber = multiplexer.GetSubscriber();
    }

    public IDatabase Database { get; }

    public ISubscriber Subscriber { get; }
}
