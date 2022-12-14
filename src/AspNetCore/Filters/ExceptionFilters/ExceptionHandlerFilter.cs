using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Fwks.AspNetCore.Abstractions.ErrorHandlers;
using Fwks.Core.Extensions;
using Fwks.AspNetCore.Results;
using Fwks.AspNetCore.ErrorHandlers;

namespace Fwks.AspNetCore.Filters.ExceptionFilters;

public sealed class ExceptionHandlerFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionHandlerFilter> _logger;
    private readonly List<IExceptionHandler<Exception>> _handlers;

    public ExceptionHandlerFilter(
        ILogger<ExceptionHandlerFilter> logger,
        IEnumerable<IExceptionHandler<Exception>> handlers)
    {
        _logger = logger;
        _handlers = handlers.ToList();
    }

    public void OnException(ExceptionContext context)
    {
        var handler = _handlers.Find(x => x.IsHandlerFor(context.Exception));

        handler ??= new UnexpectedErrorExceptionHandler();

        _logger.TraceCorrelatedError(handler.Message, context.Exception);

        context.Result = new ApplicationNotificationResult(handler.StatusCode, handler.Handle(context.Exception));
    }
}
