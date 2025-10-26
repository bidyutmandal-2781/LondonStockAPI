using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Stocks.Application.Dtos;
using Stocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.Stocks.Command
{
    public class UpdateBrokerCommandHandler : IRequestHandler<UpdateBrokerCommand, BrokerDto>
    {
        private readonly StockDBContext _db;
        private readonly IMapper _mapper;
        private ILogger<UpdateBrokerCommandHandler> _logger;

        public UpdateBrokerCommandHandler(StockDBContext db, IMapper mapper, ILogger<UpdateBrokerCommandHandler> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<BrokerDto> Handle(UpdateBrokerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var broker = await _db.Broker.FindAsync(new object[] { request.Id }, cancellationToken);
                if (broker == null)
                {
                    _logger.LogWarning($"Broker {request.Id} is not found for updation");
                    return null;
                }
                   

                broker.Name = request.Dto.Name;
                await _db.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Broker {request.Id} updated successfully");
                return _mapper.Map<BrokerDto>(broker);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating broker details {request.Id}");
                throw new InvalidOperationException($"Something went wrong while deleting broker details {request.Id}.{ex.Message}");
            }
            
        }
    }
}
