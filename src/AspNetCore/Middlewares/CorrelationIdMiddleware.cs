using Fwks.Core.Contexts;
using Microsoft.AspNetCore.Builder;

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