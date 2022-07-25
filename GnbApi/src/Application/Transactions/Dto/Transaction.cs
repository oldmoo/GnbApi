namespace GnbApi.Application.Transactions.Dto;

using Common.Entities;
using GnbApi.Application.Common.Enums;

public record Transaction : Entity
{
    public string Sku { get; init; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
}
