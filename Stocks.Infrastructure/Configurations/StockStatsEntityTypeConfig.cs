using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocks.Domain.Entities;

namespace Stocks.Infrastructure.Configurations
{
    public class StockStatsEntityTypeConfig:IEntityTypeConfiguration<StockStats>
    {
        public void Configure(EntityTypeBuilder<StockStats> b)
        {
            b.HasKey(x => x.Ticker);
            b.Property(x => x.Ticker).HasMaxLength(16);
            b.Property(x => x.TotalPrice).HasColumnType("decimal(28,8)");
            b.Property(x => x.AveragePrice).HasColumnType("decimal(18,6)");
            b.Property(x => x.LastUpdatedUtc).HasColumnType("datetime2(7)");

        }
    }
}
