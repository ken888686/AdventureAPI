using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.Core.ValueObjects;

namespace AdventureAPI.Web.Controllers.Stores;

public record StoreRecord(Guid Id, string Name, Address? Address, string Logo, StoreStatus Status);
