using AdventureAPI.Core.Aggregates.UserAggregate;
using AdventureAPI.Core.Aggregates.UserAggregate.Specifications;
using AdventureAPI.Core.Interfaces;

namespace AdventureAPI.UseCases.Auth.Login;

public class LoginHandler(
    IRepository<User> repository,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordService passwordService
) : ICommandHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Check if Username exists 
        var spec = new UserByUsernameSpec(request.Username);
        var user = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (user == null)
        {
            return Result<string>.NotFound($"Username({request.Username}) not found.");
        }

        // Check if Password correct
        return passwordService.VerifyPassword(request.Password, user.PasswordHash, user.Salt)
            ? Result<string>.Success(jwtTokenGenerator.GenerateToken(user))
            : Result<string>.Invalid(new List<ValidationError> { new("Password is incorrect.") });
    }
}
