using System.Threading.Tasks;
using Fwks.Core.Domain;
using Fwks.FwksService.Core.Domain.Notifications;
using Fwks.FwksService.Core.Domain.Queries;
using Fwks.FwksService.Core.Domain.Responses;

namespace Fwks.FwksService.Core.Abstractions.Services;

public interface ICustomerService
{
    Task AddAsync(AddCustomerNotification request);
    Task<Page<CustomerResponse>> FindByFilterAsync(GetCustomerByNameQuery filter);
}