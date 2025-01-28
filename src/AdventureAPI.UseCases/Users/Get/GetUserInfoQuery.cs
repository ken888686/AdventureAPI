namespace AdventureAPI.UseCases.Users.Get;

public record GetUserInfoQuery(Guid UserId) : IQuery<Result<UserDto>>;
