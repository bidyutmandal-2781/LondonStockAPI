using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stocks.Application.Dtos;
using Stocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.Stocks.Query
{
    public class GetAllTradesQueryHandler : IRequestHandler<GetAllTradesQuery, List<TradeResultDto>>
    {
        private readonly StockDBContext _db;
        private readonly IMapper _mapper;
        private ILogger<GetAllTradesQueryHandler> _logger;

        public GetAllTradesQueryHandler(StockDBContext db, IMapper mapper, ILogger<GetAllTradesQueryHandler> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<TradeResultDto>> Handle(GetAllTradesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var trades = await _db.Trades
                .AsNoTracking()
                .OrderByDescending(t => t.TradeTimeUtc)
                .ToListAsync(cancellationToken);

                _logger.LogInformation("Fetching all trade transactions successfully");
                return _mapper.Map<List<TradeResultDto>>(trades);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while fetching all trade transactions");
                throw new InvalidOperationException($"Something went wrong while fetching all trade transactions.{ex.Message}");
            }

        }
    }
}
