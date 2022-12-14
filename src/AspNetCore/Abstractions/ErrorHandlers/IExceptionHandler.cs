using System;
using Fwks.Core.Domain;

namespace Fwks.AspNetCore.Abstractions.ErrorHandlers;

public interface IExceptionHandler<TException> where TException : Exception
{
    abstract string Message { get; }
    abstract int StatusCode { get; }

    bool IsHandlerFor(Exception exception);
    ApplicationNotification Handle(TException exception);
}