using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Fwks.Core.Extensions;
using Fwks.Security.Cryptography.Extensions;
using Fwks.Redis.Abstractions.Connectors;
using Fwks.Redis.Abstractions.Services;

namespace Fwks.Redis.Services;

public sealed class RedisService : IRedisService
{
    private readonly ILogger<RedisService> _logger;
    private readonly IRedisConnection _connection;

    public RedisService(
        ILogger<RedisService> logger,
        IRedisConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }

    public ISubscriber Subscriber { get; }

    public Task<T> GetAsync<T>(string key)
    {
        return GetEncryptedAsync<T>(key, string.Empty);
    }

    public Task<T> GetAsync<T>(string key, Func<Task<T>> fallbackTask, TimeSpan? expires = default)
    {
        return GetEncryptedAsync(key, string.Empty, fallbackTask, expires);
    }

    public async Task<T> GetEncryptedAsync<T>(string key, string encryptionKey)
    {
        var value = await _connection.Database.StringGetAsync(key);

        if (!value.HasValue)
        {
            _logger.TraceInfo($"Failed to get key ({key}) from redis.");

            return default;
        }

        return encryptionKey.IsEmpty()
            ? value.ToString().DeserializeObject<T>()
            : value.ToString().Encrypt(encryptionKey).DeserializeObject<T>();
    }

    public async Task<T> GetEncryptedAsync<T>(string key, string encryptionKey, Func<Task<T>> fallbackTask, TimeSpan? expires = default)
    {
        try
        {
            if (key.IsEmpty())
                return await fallbackTask();

            var value = await _connection.Database.StringGetAsync(key);
            var isEncrypted = encryptionKey.IsNotEmpty();

            if (value.HasValue)
                return isEncrypted
                    ? value.ToString().Decrypt(encryptionKey).DeserializeObject<T>()
                    : value.ToString().DeserializeObject<T>();

            var fallbackValue = await fallbackTask();

            if (!HasValue(fallbackValue))
                return default;

            if (key.IsNotEmpty())
            {
                if (isEncrypted)
                    await SetEncryptedAsync(key, encryptionKey, fallbackValue, expires);
                else
                    await SetAsync(key, fallbackValue, expires);
            }

            return fallbackValue;
        }
        catch (Exception ex)
        {
            _logger.TraceError($"Failed to get key ({key}) from redis.", ex);

            return await fallbackTask();
        }

        static bool HasValue(T value) => !EqualityComparer<T>.Default.Equals(value, default);
    }

    public async Task RemoveAsync(string key)
    {
        if (!await _connection.Database.KeyDeleteAsync(key))
            _logger.TraceInfo($"Failed to remove key ({key}) from redis. The operation returned false.");
    }

    public async Task RemoveAsync(params string[] keys)
    {
        var keysRemoved = await _connection.Database.KeyDeleteAsync(keys.Select(x => new RedisKey(x)).ToArray());

        if (keysRemoved != keys.Length)
            _logger.TraceInfo($"Failed to remove {keys.Length - keysRemoved} out of {keys.Length} keys from redis.");
    }

    public Task SetAsync(string key, object value, TimeSpan? expires = default)
    {
        return SetItemAsync(key, value.SerializeObject(), expires);
    }

    public Task SetEncryptedAsync(string key, string encryptionKey, object value, TimeSpan? expires = default)
    {
        return SetItemAsync(key, value.Encrypt(encryptionKey), expires);
    }

    private async Task SetItemAsync(string key, string value, TimeSpan? expires = default)
    {
        if (key.IsEmpty())
            throw new ArgumentNullException(nameof(key));

        if (!await _connection.Database.StringSetAsync(key, value, expires))
            _logger.TraceInfo($"Failed to set key ({key}) on redis.");
    }

    public void Subscribe(string channel, Action<string, string> action, string commandFlag = "None")
    {
        var redisChannel = CreateChannel(channel);

        _connection.Subscriber.Subscribe(redisChannel, (_, value) => action(redisChannel, value), commandFlag.ToEnum<CommandFlags>());
    }

    public Task SubscribeAsync(string channel, Action<string, string> action, string commandFlag = "None")
    {
        var redisChannel = CreateChannel(channel);

        return _connection.Subscriber.SubscribeAsync(redisChannel, (_, value) => action(redisChannel, value), commandFlag.ToEnum<CommandFlags>());
    }

    public void Publish(string channel, object content, string commandFlags = "None")
    {
        var redisChannel = CreateChannel(channel);

        _connection.Subscriber.Publish(redisChannel, content.SerializeObject(), commandFlags.ToEnum<CommandFlags>());
    }

    public Task PublishAsync(string channel, object content, string commandFlags = "None")
    {
        var redisChannel = CreateChannel(channel);

        return _connection.Subscriber.PublishAsync(redisChannel, content.SerializeObject(), commandFlags.ToEnum<CommandFlags>());
    }

    private static RedisChannel CreateChannel(string channel) => new(channel, RedisChannel.PatternMode.Auto);
}
