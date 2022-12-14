using System;
using System.Threading;
using System.Threading.Tasks;
using Fwks.Core.Abstractions.Contexts;
using Fwks.Core.Abstractions.Services.Infra;
using Fwks.Core.Extensions;
using Fwks.FwksService.Core.Domain;
using Fwks.FwksService.Core.Domain.Notifications;
using Fwks.FwksService.Core.Abstractions.Repositories;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Fwks.FwksService.Application.Handlers.Customers.Notifications;

public sealed class AddCustomerPostgresNotificationHandler : INotificationHandler<AddCustomerNotification>
{
    private readonly ILogger<AddCustomerPostgresNotificationHandler> _logger;
    private readonly INotificationContext _notifications;
    private readonly ICustomerRepository<Customer, int> _repository;
    private readonly ITransactionService _transaction;

    public AddCustomerPostgresNotificationHandler(
        ILogger<AddCustomerPostgresNotificationHandler> logger,
        INotificationContext notifications,
        ICustomerRepository<Customer, int> repository,
        ITransactionService transaction)
    {
        _logger = logger;
        _notifications = notifications;
        _repository = repository;
        _transaction = transaction;
    }

    public async ValueTask Handle(AddCustomerNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            _ = DateOnly.TryParse(notification.DateOfBirth, out var dateOfBirth);

            await _repository.AddAsync(new()
            {
                Name = notification.Name,
                Email = notification.Email,
                DateOfBirth = dateOfBirth,
                PhoneNumber = notification.PhoneNumber,
            });

            if (await _transaction.CommitAsync(cancellationToken))
                _logger.TraceCorrelatedInfo($"Added customer to postgres database at {DateTime.UtcNow:HH:mm:ss}");
        }
        catch (Exception ex)
        {
            _notifications.Add(ex);
        }
    }
}