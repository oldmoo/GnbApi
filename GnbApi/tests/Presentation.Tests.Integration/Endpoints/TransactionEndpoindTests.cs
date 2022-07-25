namespace GnbApi.Presentation.Tests.Integration.Endpoints;

using System.Net;
using Application.Common.Enums;
using Application.Transactions.Dto;
using Extensions;
using Shouldly;
using Xunit;

public class TransactionEndpointTests
{
    private static readonly ApiApplication Application = new();

    [Fact]
    private async Task GetTransactions_ShouldReturn_Ok()
    {
        // Arrange
        using var client = Application.CreateClient();

        // Act
        using var response = await client.GetAsync("/api/transactions");
        var result = (await response.Content.ReadAsStringAsync()).Deserialize<List<Transaction>>();
        var count = result.Count;
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(count);

    }

    [Fact]
    private async Task GetTransactionBySku_ShouldReturn_Ok()
    {
        // Arrange
        using var client = Application.CreateClient();
        using var transactionResponse = await client.GetAsync("/api/transactions");
        var transactionResult = (await transactionResponse.Content.ReadAsStringAsync()).Deserialize<List<Transaction>>()[0];

        // Act
        using var response =
            await client.GetAsync($"/api/transactions/{transactionResult.Sku}/{transactionResult.Currency}");
        var result = (await response.Content.ReadAsStringAsync()).Deserialize<TransactionBySku>();
        var count = result.Transactions.Count;
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBeNull();
        result.Currency.ShouldBe(transactionResult.Currency);
        result.Transactions.ShouldNotBeEmpty();
        result.Transactions.Count.ShouldBe(count);

        foreach (var transaction in result.Transactions)
        {
            transaction.Amount.ShouldBeOfType<decimal>();
            transaction.Currency.ShouldBeOfType<Currency>();
            transaction.Sku.ShouldBe(transactionResult.Sku);
        }
    }
}
