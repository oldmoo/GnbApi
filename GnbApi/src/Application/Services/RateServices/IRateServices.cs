namespace GnbApi.Application.Services.RateServices;

using GnbApi.Application.Rates.Dto;

public interface IRateServices
{
    Task<List<Rates>> GetRates(CancellationToken cancellationToken);
}
