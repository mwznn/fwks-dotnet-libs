using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Fwks.Core.Abstractions.Contexts;
using Fwks.AspNetCore.Results;

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
            context.Result = new ApplicationNotificationResult(_notificationContext);

        await next();
    }
}