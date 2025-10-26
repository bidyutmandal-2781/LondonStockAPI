using MediatR;
using Stocks.Application.Dtos;

namespace Stocks.Application.Stocks.Command
{
    public class CreateTradeCommand:IRequest<TradeResultDto>
    {
        public TradeCreateDto Dto { get;}
        public CreateTradeCommand(TradeCreateDto dto)
        {
            Dto = dto;
        }
    }
}
