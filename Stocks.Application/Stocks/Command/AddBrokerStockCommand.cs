using MediatR;
using Stocks.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.Stocks.Command
{
    public class AddBrokerStockCommand:IRequest<BrokerStockInfoDto>
    {
        public string BrokerId { get; }
        public AddBrokerStockDto Dto { get; }

        public AddBrokerStockCommand(string brokerId, AddBrokerStockDto dto)
        {
            BrokerId = brokerId;
            Dto = dto;
        }
    }
}
