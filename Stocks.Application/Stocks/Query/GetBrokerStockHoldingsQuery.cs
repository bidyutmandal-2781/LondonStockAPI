using MediatR;
using Stocks.Application.Dtos;

namespace Stocks.Application.Stocks.Query
{
    public class GetBrokerStockHoldingsQuery : IRequest<List<BrokerStockInfoDto>>
    {
        public string BrokerId { get; }
        public GetBrokerStockHoldingsQuery(string brokerId) => BrokerId = brokerId;
    }
}
