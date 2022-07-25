namespace GnbApi.Infrastructure.Databases.RatesTransactions.Mapping;

using AutoMapper;
using Infrastructure = Entities;
using Application = Application.Transactions.Dto;
public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        this.CreateMap<Infrastructure.Transaction, Application.Transaction>().ReverseMap();
    }
}
