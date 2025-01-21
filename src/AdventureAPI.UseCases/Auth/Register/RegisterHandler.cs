using AdventureAPI.Core.Aggregates.UserAggregate;
using AdventureAPI.Core.Aggregates.UserAggregate.Specifications;
using AdventureAPI.Core.Interfaces;

namespace AdventureAPI.UseCases.Auth.Register;

public class RegisterHandler(
    IRepository<User> repository,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordService passwordService
) : ICommandHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Check if Username exists 
        var spec = new UserByUsernameSpec(request.Username);
        var user = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (user != null)
        {
            return Result<string>.Conflict($"{request.Username} already exists.");
        }

        // Hash password        
        var passwordHash = passwordService.Hash(request.Password, out var salt);

        // Add to database
        var newUser = new User(
            request.Username,
            request.Email,
            passwordHash,
            salt,
            request.FirstName,
            request.LastName);
        var createdUser = await repository.AddAsync(newUser, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        // Generate JWT
        return Result<string>.Created(jwtTokenGenerator.GenerateToken(createdUser));
    }
}
