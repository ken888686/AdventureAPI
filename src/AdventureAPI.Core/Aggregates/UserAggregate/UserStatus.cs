namespace AdventureAPI.Core.Aggregates.UserAggregate;

public class UserStatus(string name, int value) : SmartEnum<UserStatus>(name, value)
{
    public static readonly UserStatus Active = new(nameof(Active), 1);
    public static readonly UserStatus Inactive = new(nameof(Inactive), 2);
    public static readonly UserStatus Suspended = new(nameof(Suspended), 2);
}
