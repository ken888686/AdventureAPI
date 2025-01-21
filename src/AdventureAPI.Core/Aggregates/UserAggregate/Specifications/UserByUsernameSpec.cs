namespace AdventureAPI.Core.Aggregates.UserAggregate.Specifications;

public sealed class UserByUsernameSpec : Specification<User>
{
    public UserByUsernameSpec(string username)
    {
        Query.Where(x => x.Username.Equals(username));
    }
}
