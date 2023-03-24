using System;

namespace Fwks.FwksService.Core.Models.Responses;

public sealed record CustomerResponse(Guid Guid, string Name, string DateOfBirth, string Email, string PhoneNumber);