using MediatR;
using Stocks.Application.Dtos;

namespace Stocks.Application.Stocks.Query
{
    public class GetAllStockStatsQuery: IRequest<List<StockStatsDto>> { }
}
