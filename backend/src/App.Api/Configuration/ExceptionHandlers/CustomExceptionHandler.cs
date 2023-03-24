using Fwks.AspNetCore.ErrorHandlers;
using Fwks.FwksService.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Fwks.FwksService.App.Api.Configuration.ExceptionHandlers;

public class CustomExceptionHandler : ExceptionHandler<CustomException>
{
    public override string Message => "A custome exception handler with bad request response";
    public override int StatusCode => StatusCodes.Status400BadRequest;
}