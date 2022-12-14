using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Fwks.Core.Constants;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;
using Fwks.AspNetCore.Results;

namespace Fwks.AspNetCore.Filters.ActionFilters;

public sealed class RequestValidationActionFilter : IActionFilter
{
    private readonly ILogger<RequestValidationActionFilter> _logger;

    public RequestValidationActionFilter(
        ILogger<RequestValidationActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;

        List<Notification> notifications = context.ModelState.Values
            .Where(v => v.Errors.Count > 0)
            .SelectMany(x => x.Errors)
            .Select(x => new Notification { Message = x.ErrorMessage })
            .ToList();

        _logger.TraceCorrelatedError(ApplicationMessages.REQUESTS_VALIDATION_FAILED, notifications.Select(x => x.ToString()));

        context.Result = new ApplicationNotificationResult(StatusCodes.Status400BadRequest, ApplicationMessages.REQUESTS_VALIDATION_FAILED, notifications);
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
