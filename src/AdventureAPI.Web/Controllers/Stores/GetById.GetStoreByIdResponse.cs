using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Controllers.Stores;

public class GetStoreByIdResponse(
    StoreRecord store,
    int statusCode = StatusCodes.Status200OK
) : ApiResponse<StoreRecord>(store, statusCode)
{
}
