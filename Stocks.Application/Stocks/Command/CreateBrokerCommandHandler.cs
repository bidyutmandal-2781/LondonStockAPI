using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Stocks.Application.Dtos;
using Stocks.Domain.Entities;
using Stocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.Stocks.Command
{
    public class CreateBrokerCommandHandler : IRequestHandler<CreateBrokerCommand, BrokerDto>
    {
        private readonly StockDBContext _db;
        private readonly IMapper _mapper;
        private ILogger<AddBrokerStockCommandHandler> _logger;

        public CreateBrokerCommandHandler(StockDBContext db, IMapper mapper, ILogger<AddBrokerStockCommandHandler> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BrokerDto> Handle(CreateBrokerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var broker = _mapper.Map<Broker>(request.Dto);
                _db.Broker.Add(broker);
                await _db.SaveChangesAsync(cancellationToken);
                return _mapper.Map<BrokerDto>(broker);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while saving the broker details in database");
                throw new InvalidOperationException($"Something went wrong while saving the broker details in database.{ex.Message}");
            }
        }
    }
}
