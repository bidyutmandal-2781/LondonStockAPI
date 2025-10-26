using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.Stocks.Command
{
    public class DeleteBrokerCommand: IRequest<bool>
    {
        public string Id { get; }
        public DeleteBrokerCommand(string id) => Id = id;
    }
}
