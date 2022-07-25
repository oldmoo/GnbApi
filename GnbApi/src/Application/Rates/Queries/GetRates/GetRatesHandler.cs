namespace GnbApi.Application.Rates.Queries.GetRates;

using Dto;
using MediatR;

public class GetRatesHandler : IRequestHandler<GetRatesQuery, List<Rates>>
{
    private readonly IRatesRepository ratesRepository;

    public GetRatesHandler(IRatesRepository ratesRepository)
    {
        this.ratesRepository = ratesRepository;
    }

    public async Task<List<Rates>> Handle(GetRatesQuery request, CancellationToken cancellationToken)
    {
        return await this.ratesRepository.GetRates(cancellationToken);
    }
}
