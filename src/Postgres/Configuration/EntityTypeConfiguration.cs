using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fwks.Core.Domain;

namespace Fwks.Postgres.Configuration;

public abstract class EntityTypeConfiguration<TEntity, TKeyType> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity<TKeyType>
    where TKeyType : struct
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Ignore(x => x.Notifications);
    }
}

public class CreditCardPaymentConfiguration : IEntityTypeConfiguration<CreditCardPayment>
{
    public void Configure(EntityTypeBuilder<CreditCardPayment> builder)
    {
        builder
            .OwnsOne(x => x.PaymentInfo);
    }
}

public class Payment
{

}

public class CreditCardPayment
{
    public string PropertyX { get; set; }
    public Payment PaymentInfo { get; set; }
}