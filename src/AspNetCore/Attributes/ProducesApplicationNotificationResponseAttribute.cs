using Microsoft.AspNetCore.Mvc;
using Fwks.Core.Domain;

namespace Fwks.AspNetCore.Attributes;

public sealed class ProducesApplicationNotificationResponseAttribute : ProducesResponseTypeAttribute
{
    public ProducesApplicationNotificationResponseAttribute(int statusCode)
        : base(typeof(ApplicationNotification), statusCode)
    {

    }
}