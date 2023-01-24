using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Fwks.Core.Extensions;
using Microsoft.Extensions.Primitives;

namespace Fwks.Core.Contexts;

public static class CorrelationContext
{
    private static readonly AsyncLocal<string> _id = new();

    public static string PropertyName => "CorrelationId";
    public static string HeaderPropertyName => $"X-{PropertyName.ToSlugCase(false)}";

    public static string Id
    {
        get
        {
            return _id.Value;
        }

        set
        {
            if (value.IsEmpty())
                throw new ArgumentException($"{nameof(Id)} cannot be null or empty.", nameof(Id));

            if (!_id.Value.IsEmpty())
                return;

            _id.Value = value;
        }
    }

    public static void Setup(IDictionary<string, StringValues> requestHeaders, IDictionary<string, StringValues> responseHeaders)
    {
        requestHeaders.TryGetValue(HeaderPropertyName, out var correlationId);

        Id = correlationId.FirstOrDefault()?.ToString() ?? Guid.NewGuid().ToString();

        responseHeaders.Add(HeaderPropertyName, Id);
    }
}