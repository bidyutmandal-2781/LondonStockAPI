using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stocks.Application.Dtos;
using Stocks.Application.Stocks.Command;
using Stocks.Application.Stocks.Query;

namespace LondonStockAPI.Controllers
{
    [Route("api/brokers")]
    [ApiController]
    public class BrokersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrokersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBrokerDto dto)
            => Ok(await _mediator.Send(new CreateBrokerCommand(dto)));

        [HttpGet("with-stocks")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllBrokersWithStocksQuery()));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateBrokerDto dto)
        {
            var result = await _mediator.Send(new UpdateBrokerCommand(id, dto));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _mediator.Send(new DeleteBrokerCommand(id));
            return success ? NoContent() : NotFound();
        }

        // Add stock to broker
        [HttpPost("{brokerId}/stocks")]
        public async Task<IActionResult> AddStock(string brokerId, [FromBody] AddBrokerStockDto dto)
            => Ok(await _mediator.Send(new AddBrokerStockCommand(brokerId, dto)));

        // Get broker stock holdings
        [HttpGet("{brokerId}/stocks")]
        public async Task<IActionResult> GetStocks(string brokerId)
        {
            var result = await _mediator.Send(new GetBrokerStockHoldingsQuery(brokerId));
            return result is null ? NoContent() : Ok(result);
        }
             
    }
}
