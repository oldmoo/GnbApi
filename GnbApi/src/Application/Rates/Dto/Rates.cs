namespace GnbApi.Application.Rates.Dto;

using Common.Entities;
using GnbApi.Application.Common.Enums;

public record Rates : Entity
{
    public Currency From { get; init; }
    public Currency To { get; init; }
    public decimal Rate { get; init; }
}
