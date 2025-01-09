using AdventureAPI.UseCases.Stores;
using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Stores;

public class CreateStoreResponse(
    StoreDto store,
    string message
) : ApiResponse<StoreDto>(store, StatusCodes.Status201Created, message)
{
}
