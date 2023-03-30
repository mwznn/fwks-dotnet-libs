using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Fwks.Core.Abstractions.Mappers;
using Fwks.Core.Domain;

namespace Fwks.Core.Mappers;

public sealed class DefaultNotificationContextCodeMapper : INotificationContextCodeMapper
{
    private readonly IDictionary<string, int> _statusCodes;
    private readonly int _defaultStatusCode;

    public DefaultNotificationContextCodeMapper()
    {
        _statusCodes = Enum.GetValues<HttpStatusCode>().Distinct().Select(code => (int)code).ToDictionary(x => x.ToString(), x => x);
        _defaultStatusCode = (int)HttpStatusCode.InternalServerError;
    }

    public int Get(IReadOnlyCollection<Notification> notifications)
    {
        if (notifications.Count == 0)
            return _defaultStatusCode;

        var codes = notifications.Select(x => x.Code).Distinct().ToList();

        var code = codes.Count > 1 ? codes.OrderByDescending(x => x).First() : codes.First();

        if (_statusCodes.TryGetValue(code, out var statusCode))
            return statusCode;

        return _defaultStatusCode;
    }
}