using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.Core.ValueObjects;

namespace AdventureAPI.UseCases.Stores.Create;

public record CreateStoreCommand(
    string Name,
    Address Address,
    string CreateUser,
    string? Logo = ""
) : ICommand<Result<Store>>;
