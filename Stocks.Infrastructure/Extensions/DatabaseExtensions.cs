using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Stocks.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StockDBContext>();
            await context.Database.MigrateAsync();

            await SeedAsync(context);
        }

        private static async Task SeedAsync(StockDBContext context)
        {
            await SeedTradeAsync(context);
            await SeedStockStatsAsync(context);
            await SeedBrokerAsync(context);
            await SeedBrokerStockInfoAsync(context);

        }
        private static async Task SeedTradeAsync(StockDBContext context)
        {
            if (!await context.Trades.AnyAsync())
            {
                await context.Trades.AddRangeAsync(InitialData.Trades);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedStockStatsAsync(StockDBContext context)
        {
            if (!await context.StockStats.AnyAsync())
            {
                await context.StockStats.AddRangeAsync(InitialData.StockStats);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedBrokerAsync(StockDBContext context)
        {
            if (!await context.Broker.AnyAsync())
            {
                await context.Broker.AddRangeAsync(InitialData.Brokers);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedBrokerStockInfoAsync(StockDBContext context)
        {
            if (!await context.BrokerStockInfos.AnyAsync())
            {
                await context.BrokerStockInfos.AddRangeAsync(InitialData.BrokerStockInfos);
                await context.SaveChangesAsync();
            }
        }

    }
}
