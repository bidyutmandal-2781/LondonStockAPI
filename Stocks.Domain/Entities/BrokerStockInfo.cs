using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Domain.Entities
{
    public class BrokerStockInfo
    {
        public int Id { get; set; }
        public string BrokerId { get; set; } = default!;
        public string Ticker { get; set; } = default!;
        public decimal TotalStocks { get; set; }
        public decimal AvailableStocks { get; set; }
        public Broker Broker { get; set; } = default!;
    }
}
