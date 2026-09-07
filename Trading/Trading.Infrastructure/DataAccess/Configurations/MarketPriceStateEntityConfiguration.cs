using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Infrastructure.DataAccess.Entities;

namespace Trading.Infrastructure.DataAccess.Configurations;

public class MarketPriceStateEntityConfiguration : IEntityTypeConfiguration<MarketPriceStateEntity>
{
    public void Configure(EntityTypeBuilder<MarketPriceStateEntity> builder)
    {
        builder.ToTable("MarketPriceStates");

        builder.HasKey(x => x.Symbol);

        builder.Property(x => x.CurrentBidPrice).HasPrecision(18, 8);
        builder.Property(x => x.CurrentAskPrice).HasPrecision(18, 8);
        builder.Property(x => x.CurrentMarketPrice).HasPrecision(18, 8);
        builder.Property(x => x.CurrentSpread).HasPrecision(18, 8);
        builder.Property(x => x.CurrentSpreadPercent).HasPrecision(18, 8);

        builder.Property(x => x.PreviousBidPrice).HasPrecision(18, 8);
        builder.Property(x => x.PreviousAskPrice).HasPrecision(18, 8);
        builder.Property(x => x.PreviousMarketPrice).HasPrecision(18, 8);
        builder.Property(x => x.PreviousSpread).HasPrecision(18, 8);
        builder.Property(x => x.PreviousSpreadPercent).HasPrecision(18, 8);
    }
}
