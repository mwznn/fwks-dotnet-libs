using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fwks.Core.Abstractions.Channels;

public interface IBaseChannel<TMessage> where TMessage : class
{
    ValueTask AddAsync(TMessage message, CancellationToken cancellationToken = default);

    ValueTask<TMessage> ReadAsync(CancellationToken cancellationToken = default);

    IAsyncEnumerable<TMessage> ReadAllAsync(CancellationToken cancellationToken = default);

    Task<bool> TryAddAsync(TMessage message, CancellationToken cancellationToken = default);

    bool TryRead(out TMessage message);
}
