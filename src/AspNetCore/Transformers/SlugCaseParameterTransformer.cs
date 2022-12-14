using Microsoft.AspNetCore.Routing;
using Fwks.Core.Extensions;

namespace Fwks.AspNetCore.Transformers;

public sealed class SlugCaseParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        return value.ToString().ToSlugCase();
    }
}
