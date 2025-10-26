using Stocks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Infrastructure.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Trade> Trades =>
            new List<Trade>()
            {
                new Trade
                {
                    Id = Guid.NewGuid(),
                    Ticker = "BARC.L",
                    Price = 145.32m,
                    Shares = 1000,
                    BrokerId = "BROKER-ALPHA",
                    TradeTimeUtc = DateTime.UtcNow.AddHours(-2)
                },
                new Trade
                {
                    Id = Guid.NewGuid(),
                    Ticker = "VOD.L",
                    Price = 95.10m,
                    Shares = 500,
                    BrokerId = "BROKER-BETA",
                    TradeTimeUtc = DateTime.UtcNow.AddHours(-1)
                },
                new Trade
                {
                    Id = Guid.NewGuid(),
                    Ticker = "BARC.L",
                    Price = 150.00m,
                    Shares = 200,
                    BrokerId = "BROKER-ALPHA",
                    TradeTimeUtc = DateTime.UtcNow
                }
            };

        public static IEnumerable<StockStats> StockStats =>
                new List<StockStats>()
                {
                    new StockStats
                    {
                    Ticker = "BARC.L",
                    TotalPrice = 145.32m + 150.00m,
                    TradeCount = 2,
                    AveragePrice = (145.32m + 150.00m) / 2,
                    LastUpdatedUtc = DateTime.UtcNow
                    },
                    new StockStats
                    {
                        Ticker = "VOD.L",
                        TotalPrice = 95.10m,
                        TradeCount = 1,
                        AveragePrice = 95.10m,
                        LastUpdatedUtc = DateTime.UtcNow
                    }
                };
        public static IEnumerable<Broker> Brokers =>
            new List<Broker>()
            {
                new Broker { Id = "BROKER-ALPHA", Name = "Alpha Securities" },
                new Broker { Id = "BROKER-BETA", Name = "Beta Investments" }
            };
        public static IEnumerable<BrokerStockInfo> BrokerStockInfos =>
        new List<BrokerStockInfo>()
        {
            // BROKER-ALPHA holdings
            new BrokerStockInfo
            {
                BrokerId = "BROKER-ALPHA",
                Ticker = "BARC.L",
                TotalStocks = 5000m,      // Initial inventory
                AvailableStocks = 3800 // 5000 - 1000 - 200 (after trades)
            },
            new BrokerStockInfo
            {
                BrokerId = "BROKER-ALPHA",
                Ticker = "VOD.L",
                TotalStocks = 3000m,
                AvailableStocks = 3000m   // No trades yet for this broker-ticker combo
            },
            
            // BROKER-BETA holdings
            new BrokerStockInfo
            {
                BrokerId = "BROKER-BETA",
                Ticker = "VOD.L",
                TotalStocks = 2000,
                AvailableStocks = 1500   // 2000 - 500 (after trade)
            },
            new BrokerStockInfo
            {
                BrokerId = "BROKER-BETA",
                Ticker = "BARC.L",
                TotalStocks = 4000,
                AvailableStocks = 4000m   // No trades yet
            }
        };
    }
}
