using System;
using System.Threading.Tasks;
using Fwks.AspNetCore.Extensions;
using Fwks.FwksService.Core.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Fwks.FwksService.App.Api.Middlewares;

internal sealed class ErrorHandlerMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public ErrorHandlerMiddleware(
        ILogger<ErrorHandlerMiddleware> logger,
        RequestDelegate next,
        AppSettings appSettings)
    {
        _logger = logger;
        _next = next;
        _appSettings = appSettings;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await context.HandleUnexpectedExceptionAsync(_logger, ex, _appSettings.IsProduction());
        }
    }
}
