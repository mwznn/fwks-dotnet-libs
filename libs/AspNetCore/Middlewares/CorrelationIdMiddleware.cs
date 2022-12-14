using System.Threading.Tasks;
using System;
using Fwks.Core.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Fwks.AspNetCore.Extensions;

namespace Fwks.AspNetCore.Middlewares;

public static class CorrelationIdMiddleware
{
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    {
        return app
            .Use(async (context, next) =>
            {
                CorrelationContext.Setup(context.Request.Headers, context.Response.Headers);

                await next();
            });
    }
}