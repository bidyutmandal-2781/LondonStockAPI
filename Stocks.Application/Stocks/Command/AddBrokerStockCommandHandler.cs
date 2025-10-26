using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stocks.Application.Dtos;
using Stocks.Domain.Entities;
using Stocks.Infrastructure;

namespace Stocks.Application.Stocks.Command
{
    public class AddBrokerStockCommandHandler : IRequestHandler<AddBrokerStockCommand, BrokerStockInfoDto>
    {
        private readonly StockDBContext _db;
        private readonly IMapper _mapper;
        private ILogger<AddBrokerStockCommandHandler> _logger;

        public AddBrokerStockCommandHandler(StockDBContext db, IMapper mapper, ILogger<AddBrokerStockCommandHandler> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<BrokerStockInfoDto> Handle(AddBrokerStockCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var broker = await _db.Broker
                .Include(b => b.StockHoldings)
                .SingleOrDefaultAsync(b => b.Id == request.BrokerId, cancellationToken);

                if (broker == null)
                {
                    _logger.LogError("Broker not found: {BrokerId}", request.BrokerId);
                    throw new InvalidOperationException($"Broker {request.BrokerId} not found.");
                }


                var existingStock = broker.StockHoldings
                    .FirstOrDefault(s => s.Ticker == request.Dto.Ticker);

                if (existingStock != null)
                {
                    existingStock.TotalStocks += request.Dto.TotalStocks;
                    existingStock.AvailableStocks += request.Dto.TotalStocks;
                }
                else
                {
                    var newStock = new BrokerStockInfo
                    {
                        BrokerId = broker.Id,
                        Ticker = request.Dto.Ticker,
                        TotalStocks = request.Dto.TotalStocks,
                        AvailableStocks = request.Dto.TotalStocks
                    };
                    broker.StockHoldings.Add(newStock);
                    existingStock = newStock;
                }

                await _db.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Stock information for the broker {request.BrokerId} added successfully");
                return _mapper.Map<BrokerStockInfoDto>(existingStock);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching stocks information for broker {request.BrokerId}");
                throw new InvalidOperationException($"Something went wrong while fetching stocks information for broker {request.BrokerId}.{ex.Message}");
            }


        }
    }
}
