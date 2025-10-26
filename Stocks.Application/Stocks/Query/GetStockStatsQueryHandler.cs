using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Stocks.Application.Dtos;
using Stocks.Infrastructure;

namespace Stocks.Application.Stocks.Query
{
    public class GetStockStatsQueryHandler : IRequestHandler<GetStockStatsQuery, StockStatsDto?>
    {
        private readonly StockDBContext _db;
        private readonly IMapper _mapper;
        private ILogger<GetStockStatsQueryHandler> _logger;
        public GetStockStatsQueryHandler(StockDBContext db,IMapper mapper, ILogger<GetStockStatsQueryHandler> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StockStatsDto?> Handle(GetStockStatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stats = await _db.StockStats.FindAsync(request.Ticker.ToUpperInvariant());
                if (stats == null)
                {
                    _logger.LogWarning($"Stocks stat information not found for ticker {request.Ticker}");
                    return null;
                }
                    
                return _mapper.Map<StockStatsDto>(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching stock stat information for broker {request.Ticker}");
                throw new InvalidOperationException($"Something went wrong while fetching stock stat information for broker {request.Ticker}.{ex.Message}");
            }
            
        }
    }
}
