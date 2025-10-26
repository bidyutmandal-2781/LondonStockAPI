using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocks.Domain.Entities;

namespace Stocks.Infrastructure.Configurations
{
    public class TradeEntityTypeConfig : IEntityTypeConfiguration<Trade>
    {
        public void Configure(EntityTypeBuilder<Trade> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Ticker).IsRequired().HasMaxLength(16);
            b.Property(x => x.Price).HasColumnType("decimal(18,4)").IsRequired();
            b.Property(x => x.Shares).HasColumnType("decimal(18,6)").IsRequired();
            b.Property(x => x.BrokerId).HasMaxLength(64).IsRequired();
            b.Property(x => x.TradeTimeUtc).HasColumnType("datetime2(7)").IsRequired();
            b.HasIndex(x => x.Ticker);
            b.HasIndex(x => x.TradeTimeUtc);
        }
    }
}
