using AdventureAPI.Core.Aggregates.UserAggregate;
using AdventureAPI.Core.Interfaces;

namespace AdventureAPI.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        return JwtBearer.CreateToken(
            o =>
            {
                o.User.Roles.Add(user.UserRole.ToString());
                o.User.Claims.Add(("UserName", user.Username));
                o.User.Claims.Add(("UserId", user.Id.ToString()));
            });
    }
}
