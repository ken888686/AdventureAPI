using AdventureAPI.UseCases.Stores;
using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Controllers.Stores;

public class GetStoreByIdResponse(
    StoreDto store,
    string message
) : ApiResponse<StoreDto>(store, StatusCodes.Status200OK, message)
{
}
