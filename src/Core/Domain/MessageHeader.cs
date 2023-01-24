using System;

namespace Fwks.Core.Domain;

public record MessageHeader(string CorrelationId, DateTime TimeStamp);
