using System;

namespace Shared.Dtos
{
    public record AssetDto(
        Guid Id,
        string Name,
        string Broker,
        string Category,
        string Currency);
}
