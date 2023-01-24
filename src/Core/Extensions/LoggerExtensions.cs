using System;
using Fwks.Core.Constants;
using Fwks.Core.Contexts;
using Microsoft.Extensions.Logging;

namespace Fwks.Core.Extensions;

public static class LoggerExtensions
{
    public static void TraceCorrelatedUnexpectedError(this ILogger logger, Exception ex)
    {
        logger.TraceCorrelatedUnexpectedError(CorrelationContext.Id, ex);
    }

    public static void TraceCorrelatedUnexpectedError(this ILogger logger, string correlationId, Exception ex)
    {
        logger.LogError(ex, $"{ApplicationMessages.KEYS_CORRELATION_ID} {ApplicationMessages.KEYS_MESSAGE}", correlationId, ApplicationMessages.ERRORS_UNEXPECTED);
    }

    public static void TraceCorrelatedInfo(this ILogger logger, string message, object details = default)
    {
        logger.TraceCorrelatedInfo(message, CorrelationContext.Id, details);
    }

    public static void TraceCorrelatedInfo(this ILogger logger, string message, string correlationId, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Information, default, message, correlationId, details);
    }

    public static void TraceCorrelatedWarning(this ILogger logger, string message, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Warning, default, message, CorrelationContext.Id, details);
    }

    public static void TraceCorrelatedWarning(this ILogger logger, string message, string correlationId, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Warning, default, message, correlationId, details);
    }

    public static void TraceCorrelatedError(this ILogger logger, string message, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Error, default, message, CorrelationContext.Id, details);
    }

    public static void TraceCorrelatedError(this ILogger logger, string message, string correlationId, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Error, default, message, correlationId, details);
    }

    public static void TraceCorrelatedError(this ILogger logger, string message, Exception ex, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Error, ex, message, CorrelationContext.Id, details);
    }

    public static void TraceCorrelatedError(this ILogger logger, string message, string correlationId, Exception ex, object details = default)
    {
        logger.TraceCorrelated(LogLevel.Error, ex, message, correlationId, details);
    }

    public static void TraceUnexpectedError(this ILogger logger, Exception ex)
    {
        logger.LogError(ex, $"{ApplicationMessages.KEYS_MESSAGE}", ApplicationMessages.ERRORS_UNEXPECTED);
    }

    public static void TraceInfo(this ILogger logger, string message, object details = default)
    {
        logger.Trace(LogLevel.Information, default, message, details);
    }

    public static void TraceWarning(this ILogger logger, string message, object details = default)
    {
        logger.Trace(LogLevel.Warning, default, message, details);
    }

    public static void TraceError(this ILogger logger, string message, object details = default)
    {
        logger.Trace(LogLevel.Error, default, message, details);
    }

    public static void TraceError(this ILogger logger, Exception ex, string message, object details = default)
    {
        logger.Trace(LogLevel.Error, ex, message, details);
    }

    public static void TraceCorrelated(this ILogger logger, LogLevel level, Exception ex, string message, string correlationId, object details = default)
    {
        logger.Trace(level, ex, message, details, correlationId);
    }

    public static void Trace(this ILogger logger, LogLevel level, Exception ex, string message, object details = default, string correlationId = default)
    {
        if (details == default)
        {
            if (correlationId == default)
                logger.Log(level, ex, ApplicationMessages.KEYS_MESSAGE, message);
            else
                logger.Log(level, ex, $"{ApplicationMessages.KEYS_CORRELATION_ID} {ApplicationMessages.KEYS_MESSAGE}", correlationId, message);

            return;
        }

        if (correlationId == default)
            logger.Log(level, ex, $"{ApplicationMessages.KEYS_MESSAGE} {ApplicationMessages.KEYS_DETAILS}", message, details);
        else
            logger.Log(level, ex, $"{ApplicationMessages.KEYS_CORRELATION_ID} {ApplicationMessages.KEYS_MESSAGE} {ApplicationMessages.KEYS_DETAILS}", correlationId, message, details);
    }
}