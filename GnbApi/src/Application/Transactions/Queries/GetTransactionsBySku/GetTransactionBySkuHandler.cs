namespace GnbApi.Application.Transactions.Queries.GetTransactionsBySku;

using Common.Enums;
using Common.Exceptions;
using Dto;
using MediatR;

public class GetTransactionBySkuHandler : IRequestHandler<GetTransactionBySkuQuery, TransactionBySku>
{
    private readonly ITransactionRepository transactionRepository;

    public GetTransactionBySkuHandler(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public async Task<TransactionBySku> Handle(GetTransactionBySkuQuery request, CancellationToken cancellationToken)
    {
        var transactions = await this.GetTransactions(cancellationToken);
        var currency = ParsedCurrency(request?.Currency);
        if (! await this.transactionRepository.TransactionBySkuExists(transactions, request?.Sku, currency))
        {
            NotFoundException.Throw(EntityType.Transaction);
        }
        return await this.transactionRepository.GetTransactionBySku(transactions, request?.Sku, currency, cancellationToken);
    }

    private async Task<List<Transaction>> GetTransactions(CancellationToken cancellationToken)
    {
        return await this.transactionRepository.GetTransactions(cancellationToken);
    }
    private static Currency ParsedCurrency(string currency)
    {
        return Enum.TryParse<Currency>(currency, true, out var parsedValue) ? parsedValue : default;
    }

}
