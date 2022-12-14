using System;
using System.Threading.Tasks;
using Fwks.Core.Constants;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Fwks.AspNetCore.Extensions;

public static class HttpContextExtensions
{
    public static Task HandleUnexpectedExceptionAsync<T>(this HttpContext context, ILogger<T> logger, Exception ex, bool isProduction)
    {
        logger.TraceCorrelatedUnexpectedError(ex);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var exceptionMessages = isProduction ? Array.Empty<string>() : ex.ExtractMessages().ToArray();

        return context.Response.WriteAsJsonAsync(ApplicationNotification.Create(ApplicationErrorMessages.SomethingWentWrong, exceptionMessages));
    }
}