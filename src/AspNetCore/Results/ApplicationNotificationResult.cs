using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Fwks.Core.Abstractions.Contexts;
using Fwks.Core.Domain;

namespace Fwks.AspNetCore.Results;
internal class ApplicationNotificationResult : JsonResult
{
    public ApplicationNotificationResult(INotificationContext notificationContext)
        : base(ApplicationNotification.Create(notificationContext.Notifications))
    {
        ContentType = MediaTypeNames.Application.Json;
        StatusCode = notificationContext.GetStatusCode();
    }

    public ApplicationNotificationResult(int statusCode, string message, IReadOnlyCollection<Notification> notifications)
        : base(ApplicationNotification.Create(message, notifications))
    {
        ContentType = MediaTypeNames.Application.Json;
        StatusCode = statusCode;
    }

    public ApplicationNotificationResult(int statusCode, ApplicationNotification applicationNotification)
        : base(applicationNotification)
    {
        ContentType = MediaTypeNames.Application.Json;
        StatusCode = statusCode;
    }
}
