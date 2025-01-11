using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Controllers.Cities;

public class CityListResponse(IEnumerable<CityRecord> cities)
    : ApiResponse<IEnumerable<CityRecord>>(cities, StatusCodes.Status200OK)
{
}
