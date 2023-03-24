namespace Fwks.Core.Abstractions.Domain;

public interface IMessage<THeader, TBody>
    where THeader : class
    where TBody : class
{
    THeader Header { get; set; }
    TBody Body { get; set; }
}
