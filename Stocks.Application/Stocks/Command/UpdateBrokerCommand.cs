using MediatR;
using Stocks.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.Stocks.Command
{
    public class UpdateBrokerCommand:IRequest<BrokerDto>
    {
        public string Id { get; }
        public UpdateBrokerDto Dto { get; }

        public UpdateBrokerCommand(string id, UpdateBrokerDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
