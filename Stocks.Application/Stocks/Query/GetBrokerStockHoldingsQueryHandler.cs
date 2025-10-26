using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stocks.Application.Dtos;
using Stocks.Infrastructure;

namespace Stocks.Application.Stocks.Query
{
    public class GetBrokerStockHoldingsQueryHandler: IRequestHandler<GetBrokerStockHoldingsQuery, List<BrokerStockInfoDto>>
    {
        private readonly StockDBContext _db;
        private readonly IMapper _mapper;
        private ILogger<GetBrokerStockHoldingsQueryHandler> _logger;

        public GetBrokerStockHoldingsQueryHandler(StockDBContext db, IMapper mapper, ILogger<GetBrokerStockHoldingsQueryHandler> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BrokerStockInfoDto>> Handle(GetBrokerStockHoldingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var holdings = await _db.BrokerStockInfos
                .Where(b => b.BrokerId == request.BrokerId)
                .ToListAsync(cancellationToken);

                if(holdings == null || !holdings.Any())
                {
                    _logger.LogWarning($"No stocks details found for broker {request.BrokerId}");
                    return null;
                }

                _logger.LogInformation($"Fetched all stocks holding for broker {request.BrokerId}");
                return _mapper.Map<List<BrokerStockInfoDto>>(holdings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while fetching stocks holding for broker {request.BrokerId}");
                throw new InvalidOperationException($"Something went wrong while fetching stocks holding for broker.{ex.Message}");
            }
            
        }
    }
}
