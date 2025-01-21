using AdventureAPI.UseCases.Stores;
using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Controllers.Stores;

public class CreateStoreResponse(
    StoreDto? store,
    int statusCode = StatusCodes.Status201Created,
    IEnumerable<string>? messages = null
) : ApiResponse<StoreDto?>(store, statusCode, messages)
{
}
