using Microsoft.AspNetCore.Builder;
using Fwks.Core.Contexts;

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