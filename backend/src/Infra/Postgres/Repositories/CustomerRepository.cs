using Fwks.FwksService.Core.Abstractions.Repositories;
using Fwks.FwksService.Core.Entities;
using Fwks.FwksService.Infra.Postgres.Contexts;
using Fwks.Postgres.Repositories;

namespace Fwks.FwksService.Infra.Postgres.Repositories;

public sealed class CustomerRepository : BaseRepository<Customer, int>, ICustomerRepository<Customer, int>
{
    public CustomerRepository(
        AppServiceContext dbContext) : base(dbContext)
    {
    }
}