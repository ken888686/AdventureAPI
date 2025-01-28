namespace AdventureAPI.Web.Controllers.Users;

public class GetUserByIdRequest
{
    public const string Route = "/user/{UserId:Guid}";
    public Guid UserId { get; init; }
}
