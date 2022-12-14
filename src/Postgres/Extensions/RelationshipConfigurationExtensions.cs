using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fwks.Core.Domain;

namespace Fwks.Postgres.Extensions;

public static class RelationshipConfigurationExtensions
{
    public static void DisableCascade<TEntity, TRelatedEntity, TKeyType>(this ReferenceReferenceBuilder<TEntity, TRelatedEntity> builder)
        where TEntity : Entity<TKeyType>
        where TRelatedEntity : Entity<TKeyType>
        where TKeyType : struct
    {
        builder.OnDelete(DeleteBehavior.Restrict);
    }

    public static void DisableCascade<TEntity, TRelatedEntity, TKeyType>(this ReferenceCollectionBuilder<TEntity, TRelatedEntity> builder)
        where TEntity : Entity<TKeyType>
        where TRelatedEntity : Entity<TKeyType>
        where TKeyType : struct
    {
        builder.OnDelete(DeleteBehavior.Restrict);
    }
}