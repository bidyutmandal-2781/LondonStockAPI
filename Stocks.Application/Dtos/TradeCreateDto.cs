namespace Stocks.Application.Dtos
{
    public record TradeCreateDto
    (
        string Ticker,
        decimal Price,
        decimal Shares,
        string BrokerId,
        string? ExternalTradeId
    );
}
