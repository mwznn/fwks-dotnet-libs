using System;
using System.Threading.Tasks;
using Fwks.Core.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Fwks.AspNetCore.Extensions;

public static class JwtBearerEventsExtensions
{
    public static Func<JwtBearerChallengeContext, Task> OnChallenge()
    {
        return (context) =>
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.StatusCode != StatusCodes.Status401Unauthorized)
                    return Task.CompletedTask;

                context.HandleResponse();

                return context.Response.WriteAsJsonAsync(ApplicationNotification.Create("Failed to authorize the request.", "Failed to validate token."));
            });

            return Task.CompletedTask;
        };
    }
}