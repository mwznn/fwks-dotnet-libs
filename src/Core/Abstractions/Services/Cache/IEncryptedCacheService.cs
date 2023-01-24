using System;
using System.Threading.Tasks;

namespace Fwks.Core.Abstractions.Services.Cache;

public interface IEncryptedCacheService
{
    Task<T> GetEncryptedAsync<T>(string key, string encryptionKey);
    Task<T> GetEncryptedAsync<T>(string key, string encryptionKey, Func<Task<T>> fallbackTask, TimeSpan? expires = default);
    Task SetEncryptedAsync(string key, string encryptionKey, object value, TimeSpan? expires = default);
}
