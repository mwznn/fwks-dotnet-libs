using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Fwks.MongoDb.Abstractions;
using Fwks.Core.Abstractions.Repositories;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;

namespace Fwks.MongoDb.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity, Guid>, IMongoRepository<TEntity>
    where TEntity : Entity<Guid>
{
    protected BaseRepository(
        IMongoDatabase database,
        string collection = default)
    {
        Collection = database.GetCollection<TEntity>(collection ?? typeof(TEntity).Name.ToCamelCase());
    }

    public IMongoCollection<TEntity> Collection { get; }

    public virtual Task AddAsync(TEntity entity)
    {
        return Collection.InsertOneAsync(entity);
    }

    public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return Collection.Find(predicate).AnyAsync();
    }

    public virtual Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return Collection.Find(predicate).ToListAsync();
    }

    public virtual Task<TEntity> FindOneByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return Collection.Find(predicate).FirstOrDefaultAsync();
    }

    public virtual Task<Page<TEntity>> FindPageByAsync(int currentPage, int pageSize, Expression<Func<TEntity, bool>> predicate = null)
    {
        return FindPageByAsync(currentPage, pageSize, predicate ?? FilterDefinition<TEntity>.Empty);
    }

    public virtual async Task<Page<TEntity>> FindPageByAsync(int currentPage, int pageSize, FilterDefinition<TEntity> filter)
    {
        var items = await Collection.Find(filter).Skip((currentPage - 1) * pageSize).Limit(pageSize).ToListAsync();

        var totalItems = (int)await Collection.CountDocumentsAsync(filter);

        return new Page<TEntity>
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            Items = items,
            TotalItems = totalItems
        };
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        return Collection.Find(FilterDefinition<TEntity>.Empty).ToListAsync();
    }

    public virtual Task<TEntity> GetByIdAsync(Guid id)
    {
        return Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public virtual Task RemoveAsync(TEntity entity)
    {
        return Collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
    }

    public virtual Task RemoveByIdAsync(Guid id)
    {
        return Collection.FindOneAndDeleteAsync(x => x.Id == id);
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        return Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }
}