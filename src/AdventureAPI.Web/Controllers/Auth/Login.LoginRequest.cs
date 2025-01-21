namespace AdventureAPI.Web.Controllers.Auth;

public class LoginRequest
{
    public const string Route = "/auth/login";

    public required string Username { get; init; }
    public required string Password { get; init; }
}
