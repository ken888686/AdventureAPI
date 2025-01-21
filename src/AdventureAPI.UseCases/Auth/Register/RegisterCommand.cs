namespace AdventureAPI.UseCases.Auth.Register;

public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    string FirstName,
    string LastName
) : ICommand<Result<string>>;
