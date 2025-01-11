using AdventureAPI.Core.Enums;
using AdventureAPI.Core.ValueObjects;

namespace AdventureAPI.Web.Controllers.Stores;

public record StoreRecord(
    Guid Id,
    string Name,
    Address? Address,
    string Logo,
    StoreStatus Status
);
