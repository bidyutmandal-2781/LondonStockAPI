namespace Stocks.Application.Dtos
{
    public record TradeResultDto(
     Guid Id,
     string Ticker,
     decimal Price,
     decimal Shares,
     string BrokerId,
     DateTime TradeTimeUtc
 );
}
