namespace GnbApi.Application.Services.TransactionServices;

using Transactions.Dto;

public interface ITransactionServices
{
    Task<List<Transaction>> GetTransactions(CancellationToken cancellationToken);
}
