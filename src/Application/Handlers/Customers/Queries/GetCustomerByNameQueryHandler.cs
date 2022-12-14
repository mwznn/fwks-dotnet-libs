using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Fwks.Core.Abstractions.Contexts;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;
using Fwks.FwksService.Core.Domain;
using Fwks.FwksService.Core.Domain.Queries;
using Fwks.FwksService.Core.Domain.Responses;
using Fwks.FwksService.Core.Abstractions.Repositories;
using Fwks.FwksService.Core.Domain.Enums;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Fwks.FwksService.Application.Handlers.Customers.Queries;

public sealed class GetCustomerByNameQueryHandler : IQueryHandler<GetCustomerByNameQuery, Page<CustomerResponse>>
{
    private readonly ILogger<GetCustomerByNameQueryHandler> _logger;
    private readonly INotificationContext _notifications;
    private readonly ICustomerRepository<Customer, int> _postgresRepository;
    private readonly ICustomerRepository<CustomerDocument, Guid> _mongoRepository;

    public GetCustomerByNameQueryHandler(
        ILogger<GetCustomerByNameQueryHandler> logger,
        INotificationContext notifications,
        ICustomerRepository<Customer, int> postgresRepository,
        ICustomerRepository<CustomerDocument, Guid> mongoRepository)
    {
        _logger = logger;
        _notifications = notifications;
        _postgresRepository = postgresRepository;
        _mongoRepository = mongoRepository;
    }

    public async ValueTask<Page<CustomerResponse>> Handle(GetCustomerByNameQuery query, CancellationToken cancellationToken)
    {
        return query.DbType switch
        {
            DbType.Postgres => await FetchFromPostgres(),
            _ => await FetchFromMongoDb(),
        };

        async Task<Page<CustomerResponse>> FetchFromPostgres()
        {
            var customers = await _postgresRepository.FindPageByAsync(query.CurrentPage, query.PageSize, Predicate());

            _logger.TraceCorrelatedInfo("Finding customers by filter.", new
            {
                Filter = query,
                ResultCount = customers.Items.Count
            });

            if (!customers.Items.Any())
                _notifications.Add("404", "No Customers were found.");

            return customers.Transform(x =>
                new CustomerResponse(x.Guid, x.Name, x.DateOfBirth.ToString(), x.Email, x.PhoneNumber));

            Expression<Func<Customer, bool>> Predicate()
            {
                return query.Name.IsEmpty()
                    ? default
                    : x => x.Name.ToLower().Contains(query.Name.ToLower());
            }
        }

        async Task<Page<CustomerResponse>> FetchFromMongoDb()
        {
            var customers = await _mongoRepository.FindPageByAsync(query.CurrentPage, query.PageSize, Predicate());

            _logger.TraceCorrelatedInfo("Finding customers by filter.", new
            {
                Filter = query,
                ResultCount = customers.Items.Count
            });

            if (!customers.Items.Any())
                _notifications.Add("404", "No Customers were found.");

            return customers.Transform(x =>
                new CustomerResponse(x.Id, x.Name, x.DateOfBirth, x.Email, x.PhoneNumber));

            Expression<Func<CustomerDocument, bool>> Predicate()
            {
                return query.Name.IsEmpty()
                    ? default
                    : x => x.Name.ToLower().Contains(query.Name.ToLower());
            }
        }
    }
}