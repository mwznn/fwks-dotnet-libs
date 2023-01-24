using System;
using System.Threading.Tasks;

namespace Fwks.Core.Abstractions.Services.Cache;

public interface ICacheService
{
    Task<T> GetAsync<T>(string key);
    Task<T> GetAsync<T>(string key, Func<Task<T>> fallbackTask, TimeSpan? expires = default);
    Task RemoveAsync(string key);
    Task RemoveAsync(params string[] keys);
    Task SetAsync(string key, object value, TimeSpan? expires = default);
}
