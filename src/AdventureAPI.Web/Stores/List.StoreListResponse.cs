using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Stores;

public class StoreListResponse(IEnumerable<StoreRecord> stores)
    : ApiResponse<IEnumerable<StoreRecord>>(stores)
{
}
