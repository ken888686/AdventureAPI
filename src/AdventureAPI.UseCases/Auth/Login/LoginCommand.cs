namespace AdventureAPI.UseCases.Auth.Login;

public record LoginCommand(
    string Username,
    string Password
) : ICommand<Result<string>>;
