using Mediator;

namespace Fwks.FwksService.Core.Domain.Notifications;

public sealed record AddCustomerNotification(string Name, string DateOfBirth, string Email, string PhoneNumber) : INotification;
