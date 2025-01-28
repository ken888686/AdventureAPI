using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Controllers.Users;

public class GetUserByIdResponse(UserRecord user)
    : ApiResponse<UserRecord>(user, StatusCodes.Status200OK)
{
}
