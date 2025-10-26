using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocks.Domain.Entities;

namespace Stocks.Infrastructure.Configurations
{
    public class BrokerStockInfoEntityTypeConfig: IEntityTypeConfiguration<BrokerStockInfo>
    {
        public void Configure(EntityTypeBuilder<BrokerStockInfo> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.BrokerId).HasMaxLength(64).IsRequired();
            b.Property(x => x.Ticker).HasMaxLength(16).IsRequired();
            b.Property(x => x.TotalStocks).HasColumnType("decimal(18,6)");
            b.Property(x => x.AvailableStocks).HasColumnType("decimal(18,6)");
            b.HasIndex(x => new { x.BrokerId, x.Ticker }).IsUnique();
        }
    }
}
