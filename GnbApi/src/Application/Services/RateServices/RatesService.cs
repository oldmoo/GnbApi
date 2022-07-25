namespace GnbApi.Application.Services.RateServices;

using Common.Enums;
using Common.Exceptions;
using Newtonsoft.Json;
using Rates.Dto;

public class RaterService : IRateServices
{
    private readonly IHttpClientFactory httpClientFactory;

    public RaterService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }
    public async Task<List<Rates>> GetRates(CancellationToken cancellationToken)
    {
        using var client = this.httpClientFactory.CreateClient("RatesApi");
        using var response = await client.GetAsync($"{client.BaseAddress}", cancellationToken);
        if (!response.IsSuccessStatusCode )
        {
            NotFoundException.Throw(EntityType.Rate);
        }
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        var ratesDto = JsonConvert.DeserializeObject<List<Rates>>(responseBody);
        return ratesDto;
    }
}
