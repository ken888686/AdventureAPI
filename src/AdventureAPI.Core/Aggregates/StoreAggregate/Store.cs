using AdventureAPI.Core.Enums;
using AdventureAPI.Core.ValueObjects;

namespace AdventureAPI.Core.Aggregates.StoreAggregate;

/// <summary>
///     Store Aggregate
/// </summary>
/// <param name="name">Store name</param>
/// <param name="createUser">Username</param>
/// <param name="logo">Logo url</param>
/// <param name="link">Store url</param>
public class Store(string name, string createUser, string? logo = "", string? link = "")
    : EntityBase<Guid>, IAggregateRoot
{
    public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public Address? Address { get; private set; }
    public string Logo { get; private set; } = logo ?? string.Empty;
    public string? Link { get; private set; } = link ?? string.Empty;
    public StoreStatus Status { get; private set; } = StoreStatus.Pending;
    public DateTimeOffset CreateTime { get; init; } = DateTimeOffset.UtcNow;
    public string CreateUser { get; init; } = Guard.Against.NullOrEmpty(createUser, nameof(createUser));
    public DateTimeOffset UpdateTime { get; private set; } = DateTimeOffset.UtcNow;
    public string UpdateUser { get; private set; } = Guard.Against.NullOrEmpty(createUser, nameof(createUser));

    /// <summary>
    ///     Update store's name
    /// </summary>
    /// <param name="newName">New store name</param>
    /// <param name="updateUser">Username</param>
    public void UpdateName(string newName, string updateUser)
    {
        Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
        UpdateAuditFields(updateUser);
    }

    /// <summary>
    ///     Update store's address
    /// </summary>
    /// <param name="address">
    ///     <see cref="ValueObjects.Address" />
    /// </param>
    /// <param name="updateUser">Username</param>
    public void UpdateAddress(Address address, string updateUser)
    {
        Address = address;
        UpdateAuditFields(updateUser);
    }

    /// <summary>
    ///     Update store's logo
    /// </summary>
    /// <param name="newLogo">Logo url</param>
    /// <param name="updateUser">Username</param>
    public void UpdateLogo(string newLogo, string updateUser)
    {
        Logo = Guard.Against.NullOrEmpty(newLogo, nameof(newLogo));
        UpdateAuditFields(updateUser);
    }

    /// <summary>
    /// </summary>
    /// <param name="newStatus">
    ///     <see cref="StoreStatus" />
    /// </param>
    /// <param name="updateUser">Username</param>
    public void UpdateStatus(StoreStatus newStatus, string updateUser)
    {
        Status = newStatus;
        UpdateAuditFields(updateUser);
    }

    /// <summary>
    /// </summary>
    /// <param name="newLink">Store url</param>
    /// <param name="updateUser">Username</param>
    public void UpdateLink(string newLink, string updateUser)
    {
        Link = Guard.Against.NullOrEmpty(newLink, nameof(newLink));
        UpdateAuditFields(updateUser);
    }

    private void UpdateAuditFields(string updateUser)
    {
        UpdateTime = DateTime.UtcNow;
        UpdateUser = Guard.Against.NullOrEmpty(updateUser, nameof(updateUser));
    }
}
