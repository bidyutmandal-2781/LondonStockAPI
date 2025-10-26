using MediatR;
using Stocks.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.Stocks.Query
{
    public class GetStockStatsQuery: IRequest<StockStatsDto?>
    {
        public string Ticker { get; }
        public GetStockStatsQuery(string ticker) => Ticker = ticker;
    }
}
