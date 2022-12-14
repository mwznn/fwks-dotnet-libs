using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Fwks.AspNetCore.Filters.ActionFilters;

public sealed class FluentValidationModelStateFilter : ActionFilterAttribute
{
    private readonly ILogger<FluentValidationModelStateFilter> _logger;

    public FluentValidationModelStateFilter(
        ILogger<FluentValidationModelStateFilter> logger)
    {
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;

        var message = "Validation failed for the request.";

        List<Notification> notifications = context.ModelState.Values
            .Where(v => v.Errors.Count > 0)
            .SelectMany(x => x.Errors)
            .Select(x => new Notification { Message = x.ErrorMessage })
            .ToList();

        _logger.TraceCorrelatedError(message, notifications.Select(x => x.ToString()));

        // TODO: Create an extension for this. always json
        context.Result = new JsonResult(ApplicationNotification.Create(message, notifications))
        {
            ContentType = MediaTypeNames.Application.Json,
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}
