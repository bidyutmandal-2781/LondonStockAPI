namespace Stocks.Application.Dtos
{
    public record StockStatsDto(
     string Ticker,
     decimal TotalPrice,
     long TradeCount,
     decimal AveragePrice,
     DateTime LastUpdatedUtc
 );
}
