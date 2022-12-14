using Fwks.Core.Abstractions.Domain;

namespace Fwks.Core.Domain;

public sealed record Message<TBody> : IMessage<MessageHeader, TBody>
    where TBody : class
{
    public MessageHeader Header { get; set; }
    public TBody Body { get; set; }
}