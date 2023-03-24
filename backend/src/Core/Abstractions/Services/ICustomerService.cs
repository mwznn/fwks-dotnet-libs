using System.Threading.Tasks;
using Fwks.Core.Domain;
using Fwks.FwksService.Core.Models.Requests;
using Fwks.FwksService.Core.Models.Responses;

namespace Fwks.FwksService.Core.Abstractions.Services;

public interface ICustomerService
{
    Task AddAsync(AddCustomerRequest request);
    Task<Page<CustomerResponse>> FindByFilterAsync(GetCustomerByNameRequest filter);
}