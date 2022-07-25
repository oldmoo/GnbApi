namespace GnbApi.Application.Tests.Unit.Transactions.Query.GetTransactionBySku;

using Application.Transactions.Queries.GetTransactionsBySku;
using FluentValidation.TestHelper;
using Xunit;

public class GetTransactionBySkuValidatorTest
{
    public static readonly GetTransactionBySkuValidator Validator = new ();

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_SkuNull()
    {
        // Arrange
        var query = new GetTransactionBySkuQuery();

        // Act
        var result = Validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.Sku);
    }

    [Fact]
    public void Validator_ShouldHaveValidationErrorFor_SkuEmpty()
    {
        // Arrange
        var query = new GetTransactionBySkuQuery {Sku = string.Empty};

        // Act
        var result = Validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.Sku);
    }

    [Fact]
    public void Validator_ShouldNotHaveValidationErrorFor_Sku()
    {
        // Arrange
        var query = new GetTransactionBySkuQuery {Sku = "S8171"};

        // Act
        var result = Validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(_ => _.Sku);
        
    }
}
