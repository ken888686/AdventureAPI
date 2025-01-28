namespace AdventureAPI.Web.Controllers.Users;

public record UserRecord(
    Guid Id,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string? PhotoUrl
);
