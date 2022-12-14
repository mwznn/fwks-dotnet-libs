using System.Threading.Tasks;
using Fwks.Core.Abstractions.Contexts;
using Fwks.Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fwks.AspNetCore.Filters.ResultFilters;

public sealed class NotificationResultFilter : IAsyncResultFilter
{
    private readonly INotificationContext _notificationContext;

    public NotificationResultFilter(
        INotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notificationContext.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = _notificationContext.GetStatusCode();

            await context.HttpContext.Response.WriteAsJsonAsync(ApplicationNotification.Create(_notificationContext.Notifications));

            return;
        }

        await next();
    }
}