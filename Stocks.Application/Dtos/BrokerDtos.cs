using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.Dtos
{
    public record BrokerDto(string Id, string Name);
    public record CreateBrokerDto(string Id, string Name);
    public record UpdateBrokerDto(string Name);
    public record BrokerStockInfoDto(
    int Id,
    string BrokerId,
    string Ticker,
    decimal TotalStocks,
    decimal AvailableStocks
    );

    public record AddBrokerStockDto(
        string Ticker,
        decimal TotalStocks
    );
    public record BrokerWithStocksDto(
    string Id,
    string Name,
    List<BrokerStockInfoDto> StockHoldings
);
}
