using System;
using System.Threading;
using System.Threading.Tasks;
using Fwks.Core.Abstractions.Contexts;
using Fwks.Core.Extensions;
using Fwks.FwksService.Core.Domain;
using Fwks.FwksService.Core.Domain.Notifications;
using Fwks.FwksService.Core.Abstractions.Repositories;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Fwks.FwksService.Application.Handlers.Customers.Notifications;

public sealed class AddCustomerMongoNotificationHandler : INotificationHandler<AddCustomerNotification>
{
    private readonly ILogger<AddCustomerMongoNotificationHandler> _logger;
    private readonly INotificationContext _notifications;
    private readonly ICustomerRepository<CustomerDocument, Guid> _repository;

    public AddCustomerMongoNotificationHandler(
        ILogger<AddCustomerMongoNotificationHandler> logger,
        INotificationContext notifications,
        ICustomerRepository<CustomerDocument, Guid> repository)
    {
        _logger = logger;
        _notifications = notifications;
        _repository = repository;
    }

    public async ValueTask Handle(AddCustomerNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.AddAsync(new()
            {
                Name = notification.Name,
                Email = notification.Email,
                DateOfBirth = notification.DateOfBirth,
                PhoneNumber = notification.PhoneNumber,
            });

            _logger.TraceCorrelatedInfo($"Added customer to mongo database at {DateTime.UtcNow:HH:mm:ss}");
        }
        catch (Exception ex)
        {
            _notifications.Add(ex);
        }
    }
}