using System.Collections.Generic;
using System.Linq;
using Fwks.Core.Constants;

namespace Fwks.Core.Domain;

public sealed record ApplicationNotification
{
    public required string Message { get; set; }
    public required List<string> Notifications { get; set; } = new();

    public static ApplicationNotification Create(string message, params string[] notifications)
    {
        return new()
        {
            Message = message,
            Notifications = notifications?.ToList()
        };
    }

    public static ApplicationNotification Create(string message, IReadOnlyCollection<Notification> notifications)
    {
        return Create(message, notifications.Select(x => x.Message).ToArray());
    }

    public static ApplicationNotification Create(IReadOnlyCollection<Notification> notifications)
    {
        return Create(ApplicationMessages.ERRORS_CHECK_NOTITICATIONS, notifications.ToList());
    }

    public static ApplicationNotification InternalError()
    {
        return Create(ApplicationMessages.ERRORS_SOMETHING_WRONG, ApplicationMessages.ERRORS_UNEXPECTED);
    }
}