using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stocks.Application.Dtos;
using Stocks.Infrastructure;

namespace Stocks.Application.Stocks.Query
{
    public class GetStockStatsRangeQueryHandler : IRequestHandler<GetStockStatsRangeQuery, List<StockStatsDto>>
    {
        private readonly StockDBContext _db;
        private readonly IMapper _mapper;
        private ILogger<GetStockStatsRangeQueryHandler> _logger;
        public GetStockStatsRangeQueryHandler(StockDBContext db,IMapper mapper, ILogger<GetStockStatsRangeQueryHandler> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<StockStatsDto>> Handle(GetStockStatsRangeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tickers = request.Tickers
                            .Where(t => !string.IsNullOrWhiteSpace(t))
                            .Select(t => t.ToUpperInvariant())
                            .ToList();
                if(!tickers.Any())
                {
                    _logger.LogWarning($"No stocks information found for ticker {string.Join(",", request.Tickers)}");
                    return null;
                }

                var stats = await _db.StockStats
                                .Where(s => tickers.Contains(s.Ticker))
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);

                _logger.LogInformation($"Stock stats information fetched successfully for tickers {string.Join(",", request.Tickers)}");
                return _mapper.Map<List<StockStatsDto>>(stats);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Something went wrong while fetching stock stats information for ticker {string.Join(",", request.Tickers)}");
                throw new InvalidOperationException($"Something went wrong while fetching stock stats information for ticker {string.Join(",", request.Tickers)}. {ex.Message}");
            }
            
        }
    }
}
