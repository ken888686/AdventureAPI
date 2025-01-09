using AdventureAPI.Core.Enums;
using AdventureAPI.Core.ValueObjects;

namespace AdventureAPI.UseCases.Stores;

public record StoreDto(
    Guid Id,
    string Name,
    Address? Address,
    string Logo,
    StoreStatus Status
);
