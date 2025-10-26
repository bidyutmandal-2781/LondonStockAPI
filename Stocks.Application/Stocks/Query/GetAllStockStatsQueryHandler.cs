using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stocks.Application.Dtos;
using Stocks.Infrastructure;

namespace Stocks.Application.Stocks.Query
{
    public class GetAllStockStatsQueryHandler : IRequestHandler<GetAllStockStatsQuery, List<StockStatsDto>>
    {
        private readonly StockDBContext _db;
        private readonly IMapper _mapper;
        private ILogger<GetAllStockStatsQueryHandler> _logger;
        public GetAllStockStatsQueryHandler(StockDBContext db,IMapper mapper, ILogger<GetAllStockStatsQueryHandler> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<StockStatsDto>> Handle(GetAllStockStatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stats = await _db.StockStats.ToListAsync(cancellationToken);
                _logger.LogInformation($"Fetched all stocks stats successfully");
                return _mapper.Map<List<StockStatsDto>>(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while fetching all stocks stats");
                throw new InvalidOperationException($"Something went wrong while fetching all stocks stats.{ex.Message}");
            }
            


        }
    }
}
