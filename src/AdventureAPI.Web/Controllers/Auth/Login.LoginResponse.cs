using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Controllers.Auth;

public class LoginResponse(
    string token,
    int statusCode = StatusCodes.Status200OK,
    IEnumerable<string>? messages = null
) : ApiResponse<string>(token, statusCode, messages)
{
}
