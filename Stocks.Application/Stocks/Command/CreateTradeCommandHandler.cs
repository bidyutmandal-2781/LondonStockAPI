using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stocks.Application.Dtos;
using Stocks.Domain.Entities;
using Stocks.Infrastructure;

namespace Stocks.Application.Stocks.Command
{
    public class CreateTradeCommandHandler : IRequestHandler<CreateTradeCommand, TradeResultDto>
    {
        private readonly StockDBContext _db;
        private readonly IMapper _mapper;
        private ILogger<CreateTradeCommandHandler> _logger;
        public CreateTradeCommandHandler(StockDBContext dBContext, IMapper mapper, ILogger<CreateTradeCommandHandler> logger)
        {
            _db = dBContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TradeResultDto> Handle(CreateTradeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(
                            "Processing trade: Ticker={Ticker}, Broker={BrokerId}, Shares={Shares}, Price={Price}",
                            request.Dto.Ticker, request.Dto.BrokerId, request.Dto.Shares, request.Dto.Price);

                var trade = _mapper.Map<Trade>(request.Dto);

                _db.Trades.Add(trade);

                var broker = await _db.Broker
                .Include(b => b.StockHoldings)
                .SingleOrDefaultAsync(b => b.Id == trade.BrokerId, cancellationToken);

                if (broker == null)
                {
                    _logger.LogError("Broker not found: {BrokerId}", trade.BrokerId);
                    throw new InvalidOperationException($"Broker {trade.BrokerId} not found.");
                }


                // Check if broker has this ticker
                var stockInfo = broker.StockHoldings
                    .FirstOrDefault(s => s.Ticker == trade.Ticker);

                if (stockInfo == null)
                {
                    _logger.LogError("Broker {BrokerId} does not have ticker {Ticker}", broker.Id, trade.Ticker);
                    throw new InvalidOperationException(
                        $"Broker {trade.BrokerId} does not have ticker {trade.Ticker} available.");
                }


                // Check if enough shares available
                if (trade.Shares > stockInfo.AvailableStocks)
                {
                    _logger.LogError("Insufficient stock: Broker={BrokerId}, Ticker={Ticker}, Available={Available}, Requested={Requested}",
                     broker.Id, trade.Ticker, stockInfo.AvailableStocks, trade.Shares);

                    throw new InvalidOperationException(
                        $"Insufficient stock: Broker {broker.Name} has only {stockInfo.AvailableStocks} shares of {trade.Ticker} available.");
                }


                // Reduce available stock
                stockInfo.AvailableStocks -= trade.Shares;


                var stats = await _db.StockStats.FindAsync(trade.Ticker);
                if (stats == null)
                {
                    stats = new StockStats
                    {
                        Ticker = trade.Ticker,
                        TotalPrice = trade.Price,
                        TradeCount = 1,
                        AveragePrice = trade.Price,
                        LastUpdatedUtc = trade.TradeTimeUtc
                    };
                    _db.StockStats.Add(stats);
                    _logger.LogInformation("Created new StockStats for Ticker={Ticker}", trade.Ticker);
                }
                else
                {
                    stats.TotalPrice += trade.Price;
                    stats.TradeCount += 1;
                    stats.AveragePrice = stats.TotalPrice / stats.TradeCount;
                    stats.LastUpdatedUtc = trade.TradeTimeUtc;
                    _db.StockStats.Update(stats);
                }
                // Add trade
                _db.Trades.Add(trade);
                await _db.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Trade created successfully: TradeId={TradeId}, Ticker={Ticker}, Shares={Shares}",
                trade.Id, trade.Ticker, trade.Shares);

                return _mapper.Map<TradeResultDto>(trade);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Something went wrong while processing trade information");
                throw new InvalidOperationException($"Something went wrong while processing trade information.{ex.Message}");
            }

        }
    }
}
