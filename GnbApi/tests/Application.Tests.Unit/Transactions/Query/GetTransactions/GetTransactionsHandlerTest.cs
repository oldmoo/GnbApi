namespace GnbApi.Application.Tests.Unit.Transactions.Query.GetTransactions;

using Application.Common.Enums;
using Application.Transactions;
using Application.Transactions.Dto;
using Application.Transactions.Queries;
using NSubstitute;
using Shouldly;
using Xunit;

public class GetTransactionsHandlerTest
{
    [Fact]
    private async Task Handle_ShouldPassThrough_Query()
    {
        // Arrange
        var query = new GetTransactionsQuery();
        var context = Substitute.For<ITransactionRepository>();
        var handler = new GetTransactionsHandler(context);
        var token = new CancellationTokenSource().Token;

        _ = context.GetTransactions(token).Returns(new List<Transaction>
        {
            new Transaction { Id = Guid.Empty,Sku = "S8171", Amount = 17.0m, Currency = Currency.Usd },
            new Transaction
            {
                Id = Guid.Parse("37bd4535-0230-4891-90da-36f873b4a895"),
                Sku = "U7897",
                Amount = 24.6m,
                Currency = Currency.Eur
            }
        });

        // Act
        var result = await handler.Handle(query, token);

        // Assert
        _ = context.Received(1).GetTransactions(token);

        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(2);
        result[0].Id.ShouldBe(Guid.Empty);
        result[1].Id.ShouldBe(Guid.Parse("37bd4535-0230-4891-90da-36f873b4a895"));
        result[0].Currency.ShouldNotBe(Currency.Eur);
        result[1].Amount.ShouldBe(24.6m);
    }

}
