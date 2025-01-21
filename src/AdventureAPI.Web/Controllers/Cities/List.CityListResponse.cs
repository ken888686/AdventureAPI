using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Controllers.Cities;

public class CityListResponse(
    IEnumerable<CityRecord> cities,
    int statusCode = StatusCodes.Status200OK,
    IEnumerable<string>? messages = null
) : ApiResponse<IEnumerable<CityRecord>>(cities, statusCode, messages)
{
}
