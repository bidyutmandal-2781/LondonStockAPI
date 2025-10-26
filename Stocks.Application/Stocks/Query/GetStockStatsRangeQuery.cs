using MediatR;
using Stocks.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.Stocks.Query
{
    public class GetStockStatsRangeQuery: IRequest<List<StockStatsDto>>
    {
        public List<string> Tickers { get; }
        public GetStockStatsRangeQuery(List<string> tickers) => Tickers = tickers;
    }
}
