using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Controllers.Stores;

public class StoreListResponse(IEnumerable<StoreRecord> stores)
    : ApiResponse<IEnumerable<StoreRecord>>(stores, StatusCodes.Status200OK)
{
}
