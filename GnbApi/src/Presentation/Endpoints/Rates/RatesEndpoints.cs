namespace GnbApi.Presentation.Endpoints.Rates;

using System.Net.Mime;
using Application.Rates.Dto;
using Application.Rates.Queries.GetRates;
using Errors;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

public static class RatesEndpoints
{
    public static WebApplication MapRatesEndpoints(this WebApplication app)
    {
        _ = app.MapGet("/api/rates", GetRates)
            .WithTags("Rates")
            .WithMetadata(new SwaggerOperationAttribute("Lookup all Rates", "\n    GET /Rates"))
            .Produces<List<Rates>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);
            return app;
    }

    private static async Task<IResult> GetRates(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetRatesQuery()));
    }
}
