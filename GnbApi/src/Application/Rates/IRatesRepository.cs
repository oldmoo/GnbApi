namespace GnbApi.Application.Rates;

using Dto;

public interface IRatesRepository
{
    Task<List<Rates>> GetRates(CancellationToken cancellationToken);
  
}
