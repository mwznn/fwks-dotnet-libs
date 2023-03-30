using System.Net.Mime;
using Fwks.AspNetCore.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Fwks.AspNetCore.Extensions;

public static class MvcOptionsExtensions
{
    public static void AddJsonAsDefaultMediaType(this MvcOptions options)
    {
        options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
        options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
    }

    public static void AddApplicationNotificationResponses(this MvcOptions options, params int[] statusCodes)
    {
        foreach (var statusCode in statusCodes)
            options.Filters.Add(new ProducesApplicationNotificationResponseAttribute(statusCode));
    }
}