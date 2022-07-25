namespace GnbApi.Application.Tests.Unit.Rates.GetRates;

using Application.Common.Enums;
using Application.Rates;
using Application.Rates.Dto;
using Application.Rates.Queries.GetRates;
using NSubstitute;
using Shouldly;
using Xunit;

public class GetRatesHandlerTest
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Query()
    {
        // Arrange
        var query = new GetRatesQuery();
        var context = Substitute.For<IRatesRepository>();
        var handler = new GetRatesHandler(context);
        var token = new CancellationTokenSource().Token;

        _ = context.GetRates(token).Returns(new List<Rates>()
        {
            new Rates
            {
                Id = Guid.Empty,
                From = Currency.Aud,
                To = Currency.Eur,
                Rate = 1.89m
            },
            new Rates
            {
                Id = Guid.Empty,
                From = Currency.Cad,
                To = Currency.Aud,
                Rate = 0.96m
            },
        });

        // Act
        var result = await handler.Handle(query, token);

        // Assert
        _ = await context.Received(1).GetRates(token);

        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
        result[0].Id.ShouldBe(Guid.Empty);
        result[1].From.ShouldBe(Currency.Cad);
        result[0].Rate.ShouldBe(1.89m);
    }
}
