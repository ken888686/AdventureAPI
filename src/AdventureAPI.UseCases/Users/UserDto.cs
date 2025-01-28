namespace AdventureAPI.UseCases.Users;

public record UserDto(
    Guid UserId,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string? PhotoUrl
);
