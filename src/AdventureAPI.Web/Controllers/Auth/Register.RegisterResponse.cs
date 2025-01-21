using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Controllers.Auth;

public class RegisterResponse(
    string token,
    int statusCode = StatusCodes.Status201Created,
    IEnumerable<string>? messages = null
) : ApiResponse<string>(token, statusCode, messages)
{
}
