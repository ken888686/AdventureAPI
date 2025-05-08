namespace AdventureAPI.UseCases.Auth;

public class AuthenticationResult
{
    public bool IsAuthenticated { get; set; }
    public string Token { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}
