using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Domain.Entities
{
    public class Trade
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Ticker { get; set; } = default!; // upper-case
        public decimal Price { get; set; }
        public decimal Shares { get; set; }
        public string BrokerId { get; set; } = default!;
        public DateTime TradeTimeUtc { get; set; } = DateTime.UtcNow;
    }
}
