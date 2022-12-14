using System;
using MongoDB.Bson.Serialization;
using Fwks.Core.Domain;

namespace Fwks.MongoDb.Mappers;

public abstract class EntityClassMap<TEntity> : BsonClassMap<TEntity> where TEntity : Entity<Guid>
{
    protected EntityClassMap()
    {
        if (IsClassMapRegistered(typeof(TEntity)))
            RegisterClassMap<TEntity>(Map);
    }

    protected virtual void Map(BsonClassMap<TEntity> mapper)
    {
        mapper.AutoMap();

        mapper.UnmapProperty(x => x.Notifications);
    }
}