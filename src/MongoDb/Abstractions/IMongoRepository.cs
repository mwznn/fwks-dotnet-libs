using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Fwks.Core.Domain;

namespace Fwks.MongoDb.Abstractions;

public interface IMongoRepository<TEntity> where TEntity : Entity<Guid>
{
    Task<Page<TEntity>> FindPageByAsync(int currentPage, int pageSize, FilterDefinition<TEntity> filter);
}