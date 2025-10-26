using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stocks.Application.Dtos;
using Stocks.Application.Stocks.Command;
using Stocks.Application.Stocks.Query;

namespace LondonStockAPI.Controllers
{
    [Route("api/trades")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TradesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> PostTrade([FromBody] TradeCreateDto dto)
        {
            var result = await _mediator.Send(new CreateTradeCommand(dto));
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTrades()
        {
            var result = await _mediator.Send(new GetAllTradesQuery());
            return Ok(result);
        }
    }
}
