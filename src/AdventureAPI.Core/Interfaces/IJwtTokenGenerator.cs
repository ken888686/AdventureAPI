using AdventureAPI.Core.Aggregates.UserAggregate;

namespace AdventureAPI.Core.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
