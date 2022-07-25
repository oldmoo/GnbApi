namespace GnbApi.Application.Tests.Unit.Transactions.Query.GetTransactionBySku;

using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Transactions;
using Application.Transactions.Dto;
using Application.Transactions.Queries;
using Application.Transactions.Queries.GetTransactionsBySku;
using NSubstitute;
using Shouldly;
using Xunit;

public class GetTransactionBySkuHandlerTest
{
    [Fact]
    private async Task Handle_ShouldPassThrough_Query()
    {
        // Arrange
        var query = new GetTransactionBySkuQuery { Sku = "S8171", Currency = "eur"};
        var context = Substitute.For<ITransactionRepository>();
        var handler = new GetTransactionBySkuHandler(context);
        var token = new CancellationTokenSource().Token;
        var transactions = new List<Transaction>
        {
            new Transaction { Id = Guid.Empty, Sku = "S8171" , Amount = 28.5m, Currency = Currency.Eur},
            new Transaction { Id = Guid.Empty, Sku = "S8171" , Amount = 32.2m, Currency = Currency.Eur}
        };

        _ = context.GetTransactions(token).Returns(transactions);

        _ = context.TransactionBySkuExists(Arg.Any<List<Transaction>>(), Arg.Any<string>(), Arg.Any<Currency>())
            .Returns(true);

        _ = context.GetTransactionBySku(Arg.Any<IEnumerable<Transaction>>(), Arg.Any<string>(), Arg.Any<Currency>(),
                token)
            .Returns(new TransactionBySku
            {
                Currency = Currency.Eur,
                TotalAmount = 2405.4m,
                Transactions = transactions

            });

        // Act
        var result = await handler.Handle(query, token);

        // Assert
        _ = context.Received(1).GetTransactions(token);
        _ = context.Received(1).TransactionBySkuExists(transactions, "S8171", Currency.Eur);
        _ = await context.Received(1).GetTransactionBySku(transactions, query.Sku, Currency.Eur, token);


        result.ShouldNotBeNull();
        result.Transactions.Count.ShouldBe(2);
        result.Currency.ShouldBe(Currency.Eur);
        result.TotalAmount.ShouldNotBe(15.3m);
        result.Transactions[0].Sku.ShouldBe("S8171");
    }

    [Fact]
    private async Task Handle_ShouldThrowException_DoesNotExist()
    {
        // Arrange
        var query = new GetTransactionBySkuQuery { Sku = "K8754", Currency = "eur"};
        var context = Substitute.For<ITransactionRepository>();
        var handler = new GetTransactionBySkuHandler(context);
        var token = new CancellationTokenSource().Token;
        var transactions = new List<Transaction>
        {
            new Transaction { Id = Guid.Empty, Sku = "S8171" , Amount = 28.5m, Currency = Currency.Eur},
            new Transaction { Id = Guid.Empty, Sku = "S8171" , Amount = 32.2m, Currency = Currency.Eur}
        };

        _ = context.GetTransactions(token).Returns(transactions);

        _ = context.TransactionBySkuExists(Arg.Any<List<Transaction>>(), Arg.Any<string>(), Arg.Any<Currency>())
            .Returns(false);

        _ = context.GetTransactionBySku(Arg.Any<IEnumerable<Transaction>>(), Arg.Any<string>(), Arg.Any<Currency>(),
                token)
            .Returns(new TransactionBySku
            {
                Currency = Currency.Eur,
                TotalAmount = 2405.4m,
                Transactions = transactions

            });

        // Act
        var exception = Should.Throw<NotFoundException>(async () => await handler.Handle(query, token));

        // Assert
        _ = context.Received(1).GetTransactions(token);
        _ = context.Received(1).TransactionBySkuExists(transactions, "K8754", Currency.Eur);
        _ = await context.Received(0).GetTransactionBySku(transactions, query.Sku, Currency.Eur, token);

        exception.Message.ShouldBe("The Transaction with the supplied id was not found.");
    }
}
