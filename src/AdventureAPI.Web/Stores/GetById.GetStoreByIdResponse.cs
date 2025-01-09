using AdventureAPI.UseCases.Stores;
using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Stores;

public class GetStoreByIdResponse(
    StoreDto store,
    string message
) : ApiResponse<StoreDto>(store, message)
{
}
