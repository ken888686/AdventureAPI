using AdventureAPI.Core.Enums;
using AdventureAPI.Core.ValueObjects;

namespace AdventureAPI.Web.Stores;

public record StoreRecord(
    Guid Id,
    string Name,
    Address? Address,
    string Logo,
    StoreStatus Status
);
