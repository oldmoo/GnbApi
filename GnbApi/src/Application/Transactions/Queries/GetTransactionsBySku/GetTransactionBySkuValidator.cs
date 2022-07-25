namespace GnbApi.Application.Transactions.Queries.GetTransactionsBySku;

using Common.Enums;
using FluentValidation;

public class GetTransactionBySkuValidator : AbstractValidator<GetTransactionBySkuQuery>
{
    public GetTransactionBySkuValidator()
    {
        this.RuleFor(t => t.Sku).NotNull().NotEqual(string.Empty).WithMessage("A transaction Sku was not supplied")
            .Length(5).WithMessage("A transaction Sku must be 5 characters");
        this.RuleFor(t => t.Currency).IsEnumName(typeof(Currency), false)
            .WithMessage("A transaction currency must be in Eur");
    }
}
