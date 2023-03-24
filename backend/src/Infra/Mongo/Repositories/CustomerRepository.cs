using System;
using Fwks.FwksService.Core.Abstractions.Repositories;
using Fwks.FwksService.Core.Entities;
using Fwks.MongoDb.Repositories;
using MongoDB.Driver;

namespace Fwks.FwksService.Infra.Mongo.Repositories;

public sealed class CustomerRepository : BaseRepository<CustomerDocument>, ICustomerRepository<CustomerDocument, Guid>
{
    public CustomerRepository(
        IMongoDatabase database)
        : base(database, "customers")
    {
    }
}