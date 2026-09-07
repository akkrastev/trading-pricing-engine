using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Infrastructure.DataAccess.Entities;

namespace Trading.Infrastructure.DataAccess.Configurations;

public class OrderRejectionReasonEntityConfiguration : IEntityTypeConfiguration<OrderRejectionReasonEntity>
{
    public void Configure(EntityTypeBuilder<OrderRejectionReasonEntity> builder)
    {
        builder.ToTable("OrderRejectionReasons");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .HasConversion<string>();

        builder.HasIndex(x => x.OrderEntityId)
            .HasDatabaseName("IX_OrderRejectionReasons_OrderEntityId");
    }
}
