using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Infrastructure.Configurations
{
    public class BrokerEntityTypeConfig : IEntityTypeConfiguration<Broker>
    {
        public void Configure(EntityTypeBuilder<Broker> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasMaxLength(64)
                .IsRequired();

            b.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            b.HasMany(x => x.StockHoldings)
                .WithOne(x => x.Broker)
                    .HasForeignKey(x => x.BrokerId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
