using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Infrastructure.DataAccess.Entities;

namespace Trading.Infrastructure.DataAccess.Configurations;

public class TradingRulesEntityConfiguration : IEntityTypeConfiguration<TradingRulesEntity>
{
    public void Configure(EntityTypeBuilder<TradingRulesEntity> builder)
    {
        builder.ToTable("TradingRules");

        builder.HasKey(x => x.Version);
        builder.Property(x => x.Version).ValueGeneratedNever();
        builder.Property(x => x.MaxNotional).HasPrecision(18, 8);
        builder.Property(x => x.MaxQuantity).HasPrecision(18, 8);
        builder.Property(x => x.PriceDeviationPercent).HasPrecision(18, 8);
    }
}
