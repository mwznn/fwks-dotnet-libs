using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fwks.Core.Abstractions.Contexts;
using Fwks.Core.Abstractions.Services.Infra;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;
using Fwks.FwksService.Core.Abstractions.Repositories;
using Fwks.FwksService.Core.Abstractions.Services;
using Fwks.FwksService.Core.Entities;
using Fwks.FwksService.Core.Enums;
using Fwks.FwksService.Core.Models.Requests;
using Fwks.FwksService.Core.Models.Responses;
using Microsoft.Extensions.Logging;

namespace Fwks.FwksService.Application.Services;

internal sealed class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly INotificationContext _notifications;
    private readonly ITransactionService _transaction;
    private readonly ICustomerRepository<Customer, int> _postgresRepository;
    private readonly ICustomerRepository<CustomerDocument, Guid> _mongoRepository;

    public CustomerService(
        ILogger<CustomerService> logger,
        INotificationContext notifications,
        ITransactionService transaction,
        ICustomerRepository<Customer, int> postgresRepository,
        ICustomerRepository<CustomerDocument, Guid> mongoRepository)
    {
        _logger = logger;
        _notifications = notifications;
        _postgresRepository = postgresRepository;
        _mongoRepository = mongoRepository;
        _transaction = transaction;
    }

    public async Task AddAsync(AddCustomerRequest request)
    {
        await Task.WhenAll(
            AddToMongoAsync(request),
            AddToPostgresAsync(request));

        async Task AddToMongoAsync(AddCustomerRequest request)
        {
            try
            {
                await _mongoRepository.AddAsync(new()
                {
                    Name = request.Name,
                    Email = request.Email,
                    DateOfBirth = request.DateOfBirth,
                    PhoneNumber = request.PhoneNumber,
                });

                _logger.TraceCorrelatedInfo($"Added customer to mongo database at {DateTime.UtcNow:HH:mm:ss}");
            }
            catch (Exception ex)
            {
                _notifications.Add(ex);
            }
        }

        async Task AddToPostgresAsync(AddCustomerRequest request)
        {
            try
            {
                _ = DateOnly.TryParse(request.DateOfBirth, out var dateOfBirth);

                await _postgresRepository.AddAsync(new()
                {
                    Name = request.Name,
                    Email = request.Email,
                    DateOfBirth = dateOfBirth,
                    PhoneNumber = request.PhoneNumber,
                });

                if (await _transaction.CommitAsync())
                    _logger.TraceCorrelatedInfo($"Added customer to postgres database at {DateTime.UtcNow:HH:mm:ss}");
            }
            catch (Exception ex)
            {
                _notifications.Add(ex);
            }
        }
    }

    public async Task<Page<CustomerResponse>> FindByFilterAsync(GetCustomerByNameRequest request)
    {
        return request.DbType switch
        {
            DbType.Postgres => await FetchFromPostgres(),
            _ => await FetchFromMongoDb(),
        };

        async Task<Page<CustomerResponse>> FetchFromPostgres()
        {
            var customers = await _postgresRepository.FindPageByAsync(request.CurrentPage, request.PageSize, Predicate());

            _logger.TraceCorrelatedInfo("Finding customers by filter.", new
            {
                Filter = request,
                ResultCount = customers.Items.Count
            });

            if (!customers.Items.Any())
                _notifications.Add("404", "No Customers were found.");

            return customers.Transform(x =>
                new CustomerResponse(x.Guid, x.Name, x.DateOfBirth.ToString(), x.Email, x.PhoneNumber));

            Expression<Func<Customer, bool>> Predicate()
            {
                return request.Name.IsEmpty()
                    ? default
                    : x => x.Name.ToLower().Contains(request.Name.ToLower());
            }
        }

        async Task<Page<CustomerResponse>> FetchFromMongoDb()
        {
            var customers = await _mongoRepository.FindPageByAsync(request.CurrentPage, request.PageSize, Predicate());

            _logger.TraceCorrelatedInfo("Finding customers by filter.", new
            {
                Filter = request,
                ResultCount = customers.Items.Count
            });

            if (!customers.Items.Any())
                _notifications.Add("404", "No Customers were found.");

            return customers.Transform(x =>
                new CustomerResponse(x.Id, x.Name, x.DateOfBirth, x.Email, x.PhoneNumber));

            Expression<Func<CustomerDocument, bool>> Predicate()
            {
                return request.Name.IsEmpty()
                    ? default
                    : x => x.Name.ToLower().Contains(request.Name.ToLower());
            }
        }
    }
}
