using System;
using System.Threading.Tasks;
using Fwks.Core.Abstractions.Services.Cache;

namespace Fwks.Redis.Abstractions.Services;

public interface IRedisService : ICacheService, IEncryptedCacheService
{
    void Subscribe(string channel, Action<string, string> action, string commandFlag = "None");
    Task SubscribeAsync(string channel, Action<string, string> action, string commandFlag = "None");
    void Publish(string channel, object content, string commandFlags = "None");
    Task PublishAsync(string channel, object content, string commandFlags = "None");

}
