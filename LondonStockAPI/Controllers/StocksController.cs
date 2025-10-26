using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stocks.Application.Stocks.Query;

namespace LondonStockAPI.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StocksController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetStats(string ticker)
        {
            var result = await _mediator.Send(new GetStockStatsQuery(ticker));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllStockStatsQuery());
            return Ok(result);
        }

        [HttpPost("range")]
        public async Task<IActionResult> GetRange([FromBody] List<string> tickers)
        {
            var result = await _mediator.Send(new GetStockStatsRangeQuery(tickers));
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
