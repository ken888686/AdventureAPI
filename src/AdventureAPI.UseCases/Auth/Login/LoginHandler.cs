using AdventureAPI.Core.Aggregates.UserAggregate;
using AdventureAPI.Core.Aggregates.UserAggregate.Specifications;
using AdventureAPI.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AdventureAPI.UseCases.Auth.Login;

public class LoginHandler(
    IRepository<User> repository,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordService passwordService,
    IMemoryCache cache
) : ICommandHandler<LoginCommand, Result<string>>
{
    private const string LoginAttemptsKeyPrefix = "login_attempts_";
    private const int MaxLoginAttempts = 5;
    private const int LockoutMinutes = 15;
    private const string UserNotFoundMessage = "{0} does not exist.";
    private const string InvalidCredentialsMessage = "Password is incorrect.";
    private const string TooManyAttemptsMessage =
        "Too many failed login attempts. Please try again in {0} minutes.";

    public async Task<Result<string>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken
    )
    {
        var attemptsKey = string.Concat(
            LoginAttemptsKeyPrefix,
            request.Username.ToLowerInvariant()
        );

        // Check for too many failed attempts
        if (cache.TryGetValue(attemptsKey, out int attempts) && attempts >= MaxLoginAttempts)
        {
            return Result<string>.Invalid(
                new List<ValidationError>
                {
                    new(string.Format(TooManyAttemptsMessage, LockoutMinutes)),
                }
            );
        }

        // Check if Username exists
        var spec = new UserByUsernameSpec(request.Username);
        var user = await repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (user is null)
        {
            // Increment failed attempts
            attempts = cache.GetOrCreate(
                attemptsKey,
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(LockoutMinutes);
                    return 1;
                }
            );

            if (attempts < MaxLoginAttempts)
            {
                cache.Set(attemptsKey, attempts + 1, TimeSpan.FromMinutes(LockoutMinutes));
            }

            return Result<string>.NotFound(string.Format(UserNotFoundMessage, request.Username));
        }

        // Always perform password verification to prevent timing attacks
        var passwordValid =
            user != null
            && passwordService.VerifyPassword(request.Password, user.PasswordHash, user.Salt);

        if (!passwordValid)
        {
            // Increment failed attempts
            attempts = cache.GetOrCreate(
                attemptsKey,
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(LockoutMinutes);
                    return 1;
                }
            );

            if (attempts < MaxLoginAttempts)
            {
                cache.Set(attemptsKey, attempts + 1, TimeSpan.FromMinutes(LockoutMinutes));
            }

            return Result<string>.Invalid(
                new List<ValidationError> { new(InvalidCredentialsMessage) }
            );
        }

        // Clear failed attempts on successful login
        cache.Remove(attemptsKey);

        return Result<string>.Success(jwtTokenGenerator.GenerateToken(user!));
    }
}
