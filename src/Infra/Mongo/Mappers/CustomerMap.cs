using Fwks.FwksService.Core.Domain;
using Fwks.FwksService.Infra.Mongo.Abstractions;
using Fwks.MongoDb.Mappers;

namespace Fwks.FwksService.Infra.Mongo.Mappers;

public sealed class CustomerMap : EntityClassMap<CustomerDocument>, IEntityMap
{
}