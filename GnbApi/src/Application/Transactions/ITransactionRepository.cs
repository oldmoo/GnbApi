namespace GnbApi.Application.Transactions;

using Common.Enums;
using Dto;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetTransactions(CancellationToken cancellationToken);

    Task<TransactionBySku> GetTransactionBySku(IEnumerable<Transaction> transactions, string sku, Currency currency, CancellationToken cancellationToken);
    Task<bool> TransactionBySkuExists(IEnumerable<Transaction> transactions, string sku, Currency currency);
}
