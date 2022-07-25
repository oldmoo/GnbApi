namespace GnbApi.Infrastructure.Databases.RatesTransactions.Mapping;

using Application.Rates.Dto;
using AutoMapper;
using Infrastructure = Entities;

public class RateMappingProfile : Profile
{
    public RateMappingProfile()
    {
        _ = this.CreateMap<Infrastructure.Rates, Rates>().ReverseMap();
    }
}
