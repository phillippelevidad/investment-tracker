using System;

namespace Shared.Dtos
{
    public record TransactionDto(
        Guid Id,
        AssetDto Asset,
        string Operation,
        float Quantity,
        MoneyDto UnitPrice,
        MoneyDto Volume,
        MoneyDto LiquidVolume,
        DateTime DateTime);
}
