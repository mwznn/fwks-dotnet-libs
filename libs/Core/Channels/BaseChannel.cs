using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Fwks.Core.Abstractions.Channels;

namespace Fwks.Core.Channels;

public abstract class BaseChannel<TMessage> : IBaseChannel<TMessage> where TMessage : class
{
    public Channel<TMessage> WorkChannel { get; set; }

    public ValueTask AddAsync(TMessage message, CancellationToken cancellationToken = default)
    {
        return WorkChannel.Writer.WriteAsync(message, cancellationToken);
    }

    public IAsyncEnumerable<TMessage> ReadAllAsync(CancellationToken cancellationToken = default)
    {
        return WorkChannel.Reader.ReadAllAsync(cancellationToken);
    }

    public ValueTask<TMessage> ReadAsync(CancellationToken cancellationToken = default)
    {
        return WorkChannel.Reader.ReadAsync(cancellationToken);
    }

    public async Task<bool> TryAddAsync(TMessage message, CancellationToken cancellationToken = default)
    {
        while (await WorkChannel.Writer.WaitToWriteAsync(cancellationToken) && !cancellationToken.IsCancellationRequested)
        {
            if (WorkChannel.Writer.TryWrite(message))
                return true;
        }

        return false;
    }

    public bool TryRead(out TMessage message)
    {
        return WorkChannel.Reader.TryRead(out message);
    }
}