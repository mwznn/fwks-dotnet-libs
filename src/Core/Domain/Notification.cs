﻿using System.Net;

namespace Fwks.Core.Domain;

public record Notification
{
    public string Code { get; set; } = ((int)HttpStatusCode.BadRequest).ToString();
    public required string Message { get; set; }

    public override string ToString() => $"{Code} - {Message}";
}