using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Stocks.Application.Stocks.Command;
using Stocks.Infrastructure;

namespace Stocks.Application.Validation
{
    public class CreateTradeCommandValidator : AbstractValidator<CreateTradeCommand>
    {
        private readonly StockDBContext _dbContext;

        public CreateTradeCommandValidator(StockDBContext dBContext)
        {
            _dbContext = dBContext;

            RuleFor(x => x.Dto.Ticker)
            .NotEmpty().WithMessage("Ticker is required.")
            .MaximumLength(16);

            RuleFor(x => x.Dto.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Dto.Shares)
                .GreaterThan(0).WithMessage("Shares must be greater than 0.");

            // Broker-level validation
            RuleFor(x => x.Dto)
                .CustomAsync(async (dto, context, ct) =>
                {
                    var broker = await _dbContext.Broker
                        .Include(b => b.StockHoldings)
                        .SingleOrDefaultAsync(b => b.Id == dto.BrokerId, ct);

                    if (broker == null)
                    {
                        context.AddFailure("BrokerId", $"Broker {dto.BrokerId} not found.");
                        return;
                    }

                    var stockInfo = broker.StockHoldings.FirstOrDefault(s => s.Ticker == dto.Ticker);
                    if (stockInfo == null)
                    {
                        context.AddFailure("Ticker", $"Broker {dto.BrokerId} does not have ticker {dto.Ticker}.");
                        return;
                    }

                    if (dto.Shares > stockInfo.AvailableStocks)
                    {
                        context.AddFailure("Shares",
                            $"Insufficient stock: only {stockInfo.AvailableStocks} shares available.");
                    }
                });
        }
    }
}
