namespace GnbApi.Application.Transactions.Queries.GetTransactionsBySku;

using System.ComponentModel.DataAnnotations;
using Dto;
using MediatR;

public class GetTransactionBySkuQuery : IRequest<TransactionBySku>
{
    [Required]
    public string Sku { get; set; }
    [Required]
    public string Currency { get; set; }
}
