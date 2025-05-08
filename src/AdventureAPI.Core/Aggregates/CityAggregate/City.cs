namespace AdventureAPI.Core.Aggregates.CityAggregate;

public class City(string name, string createUser) : EntityBase<Guid>, IAggregateRoot
{
    public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public Guid CountryId { get; private set; }
    public DateTimeOffset CreateTime { get; init; } = DateTimeOffset.UtcNow;
    public string CreateUser { get; init; } =
        Guard.Against.NullOrEmpty(createUser, nameof(createUser));
    public DateTimeOffset UpdateTime { get; private set; } = DateTimeOffset.UtcNow;
    public string UpdateUser { get; private set; } =
        Guard.Against.NullOrEmpty(createUser, nameof(createUser));

    private void UpdateAuditFields(string updateUser)
    {
        UpdateTime = DateTime.UtcNow;
        UpdateUser = Guard.Against.NullOrEmpty(updateUser, nameof(updateUser));
    }
}
