namespace Stocks.Domain.Entities
{
    public class Broker
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public ICollection<BrokerStockInfo> StockHoldings { get; set; } = new List<BrokerStockInfo>();
    }
}

