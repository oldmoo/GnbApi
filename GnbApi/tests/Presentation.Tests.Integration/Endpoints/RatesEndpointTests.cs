namespace GnbApi.Presentation.Tests.Integration.Endpoints;

using System.Net;
using Application.Rates.Dto;
using Extensions;
using Shouldly;
using Xunit;

public class RatesEndpointTests
{
    private static readonly ApiApplication Application = new();

    [Fact]
    private async Task GetRates_ShouldReturn_Ok()
    {
        // Arrange
        using var client = Application.CreateClient();

        // Act
        var response = await client.GetAsync("/api/rates");
        var result = (await response.Content.ReadAsStringAsync()).Deserialize<List<Rates>>();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(6);

    }
}
