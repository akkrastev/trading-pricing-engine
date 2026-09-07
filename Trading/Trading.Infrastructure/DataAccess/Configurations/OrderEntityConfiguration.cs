using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Infrastructure.DataAccess.Entities;

namespace Trading.Infrastructure.DataAccess.Configurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Side)
            .HasConversion<string>();

        builder.Property(x => x.Source)
            .HasConversion<string>();

        builder.Property(x => x.DecisionStatus)
            .HasConversion<string>();

        builder.Property(x => x.Price)
            .HasPrecision(18, 8);

        builder.Property(x => x.Quantity)
            .HasPrecision(18, 8);

        // RulesVersion is a plain logical reference only
        // no FK

        builder.HasIndex(x => x.OrderId)
            .HasDatabaseName("IX_Orders_OrderId");

        builder.HasIndex(x => new { x.Symbol, x.Timestamp })
            .HasDatabaseName("IX_Orders_Symbol_Timestamp");

        builder.HasMany(x => x.RejectionReasons)
            .WithOne()
            .HasForeignKey(x => x.OrderEntityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
