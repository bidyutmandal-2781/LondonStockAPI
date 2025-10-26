using Microsoft.EntityFrameworkCore;
using Stocks.Domain.Entities;

namespace Stocks.Infrastructure
{
    public class StockDBContext: DbContext
    {
        public StockDBContext(DbContextOptions<StockDBContext> options):base(options)
        {
            
        }

        public DbSet<Trade> Trades => Set<Trade>();
        public DbSet<StockStats> StockStats => Set<StockStats>();
        public DbSet<Broker> Broker => Set<Broker>();
        public DbSet<BrokerStockInfo> BrokerStockInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(StockDBContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
