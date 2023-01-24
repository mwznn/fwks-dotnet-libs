using System;
using System.Threading.Channels;

namespace Fwks.Core.Channels;

public abstract class BoundedChannel<TMessage> : BaseChannel<TMessage> where TMessage : class
{
    public BoundedChannel(int capacity)
    {
        WorkChannel = Channel.CreateBounded<TMessage>(new BoundedChannelOptions(capacity)
        {
            AllowSynchronousContinuations = false,
            SingleReader = true,
            SingleWriter = true,
            FullMode = BoundedChannelFullMode.Wait
        });
    }

    public BoundedChannel(int capacity, Action<BoundedChannelOptions> optionsAction)
    {
        var options = new BoundedChannelOptions(capacity);

        optionsAction(options);

        WorkChannel = Channel.CreateBounded<TMessage>(options);
    }
}
