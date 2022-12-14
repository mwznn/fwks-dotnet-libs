using Microsoft.AspNetCore.Mvc;

namespace Fwks.AspNetCore.Attributes;

public sealed class ProducesTypedResponseAttribute<T> : ProducesResponseTypeAttribute where T : class
{
    public ProducesTypedResponseAttribute(int statusCode)
        : base(typeof(T), statusCode)
    {
    }
}