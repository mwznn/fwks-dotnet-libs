using FluentValidation;
using Fwks.Core.Extensions;
using Fwks.FwksService.Core.Domain.Notifications;

namespace Fwks.FwksService.Core.Domain.Notifications.Validators;

public class AddCustomerNotificationValidator : AbstractValidator<AddCustomerNotification>
{
    public AddCustomerNotificationValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.PhoneNumber).PhoneNumber();
    }
}
