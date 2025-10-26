using MediatR;
using Stocks.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.Stocks.Command
{
    public class CreateBrokerCommand : IRequest<BrokerDto>
    {
        public CreateBrokerDto Dto { get; }
        public CreateBrokerCommand(CreateBrokerDto dto) => Dto = dto;
    }
}
