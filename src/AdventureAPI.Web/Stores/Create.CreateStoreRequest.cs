using AdventureAPI.Core.ValueObjects;

namespace AdventureAPI.Web.Stores;

public class CreateStoreRequest
{
    public const string Route = "/stores";
    public required string Name { get; set; }
    public required Address Address { get; set; }
    public required string Logo { get; set; }
}
