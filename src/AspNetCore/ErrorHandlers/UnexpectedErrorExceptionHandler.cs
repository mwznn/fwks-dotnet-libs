using System;
using Microsoft.AspNetCore.Http;

namespace Fwks.AspNetCore.ErrorHandlers;

public class UnexpectedErrorExceptionHandler : ExceptionHandler<Exception>
{
    public override int StatusCode => StatusCodes.Status500InternalServerError;
}