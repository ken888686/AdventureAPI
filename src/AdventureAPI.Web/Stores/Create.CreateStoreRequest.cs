using AdventureAPI.Core.ValueObjects;

namespace AdventureAPI.Web.Stores;

public class CreateStoreRequest
{
    public const string Route = "/stores";
    public required string Name { get; init; }
    public required Address Address { get; init; }
    public required string Logo { get; init; }
}
