using Fwks.FwksService.Core.Entities;
using Fwks.FwksService.Infra.Mongo.Abstractions;
using Fwks.MongoDb.Mappers;

namespace Fwks.FwksService.Infra.Mongo.Mappers;

public sealed class CustomerMap : EntityClassMap<CustomerDocument>, IEntityMap
{
}