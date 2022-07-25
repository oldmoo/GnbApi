namespace GnbApi.Application.Transactions.Dto;

using Common.Enums;

public record TransactionBySku
{
    public decimal TotalAmount { get; set; }
    public Currency  Currency { get; set; }
    public List<Transaction> Transactions { get; set; } = new();
}
