namespace GnbApi.Infrastructure.Databases.RatesTransactions.Extensions;

using Application.Common.Enums;
using Application.Transactions.Dto;

public static class TransactionsBySkuInEurExtension
{
    public static TransactionBySku ToTransactionBySku(this List<Transaction> transactions, List<Transaction> transactionFilter, Currency currency )
    {
        var transactionBySkus = new TransactionBySku { Currency = currency};
        foreach (var tra in transactionFilter)
        {
            transactionBySkus.Transactions.Add(new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = Math.Round(tra.Amount, 2),
                Currency = currency,
                Sku = tra.Sku
            });
            transactionBySkus.TotalAmount = Math.Round(transactionBySkus.Transactions.Sum(tr => tr.Amount), 2);
        }

        return transactionBySkus;
    }
}
