using AdventureAPI.Core.Aggregates.UserAggregate;

namespace AdventureAPI.UseCases.Users.Get;

public class GetUserInfoHandler(IReadRepository<User> repository)
    : IQueryHandler<GetUserInfoQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.UserId, cancellationToken);
        if (entity is null)
        {
            return Result<UserDto>.NotFound("Store not found");
        }

        return Result<UserDto>.Success(
            new UserDto(
                entity.Id,
                entity.Username,
                entity.Email,
                entity.FirstName,
                entity.LastName,
                entity.PhotoUrl));
    }
}
