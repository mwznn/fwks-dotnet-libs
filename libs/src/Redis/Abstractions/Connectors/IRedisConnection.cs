using StackExchange.Redis;

namespace Fwks.Redis.Abstractions.Connectors;

public interface IRedisConnection
{
    IDatabase Database { get; }
    ISubscriber Subscriber { get; }
}
