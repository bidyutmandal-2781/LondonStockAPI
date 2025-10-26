
using AutoMapper;
using Stocks.Application.Dtos;
using Stocks.Domain.Entities;
namespace Stocks.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TradeCreateDto, Trade>()
            .ForMember(dest => dest.Ticker, opt => opt.MapFrom(src => src.Ticker.ToUpperInvariant()))
            .ForMember(dest => dest.TradeTimeUtc, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<Trade, TradeResultDto>();

        CreateMap<StockStats, StockStatsDto>();

        CreateMap<CreateBrokerDto, Broker>();

        CreateMap<UpdateBrokerDto, Broker>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        // this line ensures only non-null UpdateBrokerDto properties are mapped

        CreateMap<Broker, BrokerDto>();
        CreateMap<Broker, BrokerWithStocksDto>();
        CreateMap<BrokerStockInfo, BrokerStockInfoDto>();
    }
}
