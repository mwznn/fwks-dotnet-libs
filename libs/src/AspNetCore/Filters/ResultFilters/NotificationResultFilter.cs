using System.Threading.Tasks;
using Fwks.AspNetCore.Results;
using Fwks.Core.Abstractions.Contexts;
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
            context.Result = new ApplicationNotificationResult(_notificationContext);

        await next();
    }
}