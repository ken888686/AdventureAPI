using AdventureAPI.UseCases.Stores;
using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Stores;

public class CreateStoreResponse(
    StoreDto store,
    string message
) : ApiResponse<StoreDto>(store, message)
{
    public StoreDto Store { get; init; } = store;
}
