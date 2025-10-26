using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Domain.Entities
{
    public class StockStats
    {
        public string Ticker { get; set; } = default!;
        public decimal TotalPrice { get; set; } // sum of prices
        public long TradeCount { get; set; }
        public decimal AveragePrice { get; set; }
        public DateTime LastUpdatedUtc { get; set; }
    }
}
