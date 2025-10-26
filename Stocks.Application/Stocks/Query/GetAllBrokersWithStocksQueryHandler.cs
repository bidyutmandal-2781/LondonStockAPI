using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stocks.Application.Dtos;
using Stocks.Infrastructure;

namespace Stocks.Application.Stocks.Query
{
    public class GetAllBrokersWithStocksQueryHandler : IRequestHandler<GetAllBrokersWithStocksQuery, List<BrokerWithStocksDto>>
    {
        private readonly StockDBContext _db;
        private readonly IMapper _mapper;
        private ILogger<GetAllBrokersWithStocksQueryHandler> _logger;

        public GetAllBrokersWithStocksQueryHandler(StockDBContext db, IMapper mapper, ILogger<GetAllBrokersWithStocksQueryHandler> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BrokerWithStocksDto>> Handle(GetAllBrokersWithStocksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var brokers = await _db.Broker
                .Include(b => b.StockHoldings)
                .ToListAsync(cancellationToken);
                _logger.LogInformation($"Stocks details for all brokers fetched successfully");
                return _mapper.Map<List<BrokerWithStocksDto>>(brokers);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while fetching stocks information for all brokers");
                throw new InvalidOperationException($"Something went wrong while fetching stocks information for all brokers.{ex.Message}");
            }
            
        }
    }
}
