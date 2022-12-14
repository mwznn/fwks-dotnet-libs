using System;
using System.Threading.Channels;

namespace Fwks.Core.Channels;

public abstract class UnboundedChannel<TMessage> : BaseChannel<TMessage> where TMessage : class
{
    protected UnboundedChannel()
    {
        WorkChannel = Channel.CreateUnbounded<TMessage>(new()
        {
            AllowSynchronousContinuations = false,
            SingleReader = true,
            SingleWriter = true
        });
    }

    protected UnboundedChannel(Action<UnboundedChannelOptions> optionsAction)
    {
        var options = new UnboundedChannelOptions();

        optionsAction(options);

        WorkChannel = Channel.CreateUnbounded<TMessage>(options);
    }
}
