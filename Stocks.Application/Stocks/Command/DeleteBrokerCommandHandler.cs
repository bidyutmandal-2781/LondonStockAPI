using MediatR;
using Microsoft.Extensions.Logging;
using Stocks.Infrastructure;

namespace Stocks.Application.Stocks.Command
{
    public class DeleteBrokerCommandHandler : IRequestHandler<DeleteBrokerCommand, bool>
    {
        private readonly StockDBContext _db;
        private ILogger<DeleteBrokerCommandHandler> _logger;
        public DeleteBrokerCommandHandler(StockDBContext db, ILogger<DeleteBrokerCommandHandler> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteBrokerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var broker = await _db.Broker.FindAsync(new object[] { request.Id }, cancellationToken);
                if (broker == null)
                {
                    _logger.LogInformation($"Broker {request.Id} is not found for deletion");
                    return false;
                }



                _db.Broker.Remove(broker);
                await _db.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Broker {request.Id} deleted successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while deleting broker details {request.Id}");
                throw new InvalidOperationException($"Something went wrong while deleting broker details {request.Id}.{ex.Message}");
            }

        }
    }
}
