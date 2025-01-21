using AdventureAPI.Core.Aggregates.UserAggregate;
using AdventureAPI.Core.Interfaces;

namespace AdventureAPI.Infrastructure.Services;

public class JwtTokenGenerator(IConfiguration configuration) : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        return JwtBearer.CreateToken(
            o =>
            {
                o.ExpireAt = DateTime.UtcNow.AddDays(configuration.GetValue<int>("JWT:ExpireDays"));
                o.User.Roles.Add(user.UserRole.ToString());
                o.User.Claims.Add(("UserName", user.Username));
                o.User["UserId"] = user.Id.ToString();
            });
    }
}
