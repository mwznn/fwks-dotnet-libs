using System;
using Fwks.AspNetCore.Abstractions.ErrorHandlers;
using Fwks.Core.Constants;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;

namespace Fwks.AspNetCore.ErrorHandlers;

public abstract class ExceptionHandler<TException> : IExceptionHandler<Exception> where TException : Exception
{
    public virtual string Message => ApplicationMessages.ERRORS_SOMETHING_WRONG;
    public abstract int StatusCode { get; }

    public virtual ApplicationNotification Handle(Exception exception)
    {
        return ApplicationNotification.Create(
            Message,
            EnvironmentVariables.IsProduction() ? Array.Empty<string>() : exception.ExtractMessages().ToArray());
    }

    public bool IsHandlerFor(Exception exception)
    {
        return typeof(TException).Name.Equals(exception.GetType().Name, StringComparison.InvariantCultureIgnoreCase);
    }
}