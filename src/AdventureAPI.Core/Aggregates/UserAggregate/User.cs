using AdventureAPI.Core.ValueObjects;

namespace AdventureAPI.Core.Aggregates.UserAggregate;

public class User(
    string username,
    string email,
    string passwordHash,
    byte[] salt,
    string firstName,
    string lastName
) : EntityBase<Guid>, IAggregateRoot
{
    public string Username { get; private set; } = Guard.Against.NullOrEmpty(username, nameof(username));
    public string Email { get; private set; } = Guard.Against.NullOrEmpty(email, nameof(email));
    public string PasswordHash { get; private set; } = Guard.Against.NullOrEmpty(passwordHash, nameof(passwordHash));
    public byte[] Salt { get; private set; } = Guard.Against.Null(salt, nameof(salt));
    public string FirstName { get; private set; } = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
    public string LastName { get; private set; } = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
    public string? PhotoUrl { get; private set; }
    public Address? Address { get; private set; }
    public UserRole UserRole { get; private set; } = UserRole.User;
    public UserStatus Status { get; private set; } = UserStatus.Inactive;
    public DateTimeOffset CreateTime { get; init; } = DateTimeOffset.UtcNow;
    public string CreateUser { get; init; } = Guard.Against.NullOrEmpty(username, nameof(username));
    public DateTimeOffset UpdateTime { get; private set; } = DateTimeOffset.UtcNow;
    public string UpdateUser { get; private set; } = Guard.Against.NullOrEmpty(username, nameof(username));

    public void UpdateProfile(string firstName, string lastName, Address? address, string? photoUrl)
    {
        FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
        Address = address;
        PhotoUrl = photoUrl;
        UpdateTime = DateTimeOffset.UtcNow;
    }

    public void ChangePassword(string newPasswordHash, byte[] newSalt)
    {
        PasswordHash = Guard.Against.NullOrEmpty(newPasswordHash, nameof(newPasswordHash));
        Salt = Guard.Against.Null(newSalt, nameof(newSalt));
        UpdateTime = DateTimeOffset.UtcNow;
    }

    public void ChangeStatus(UserStatus newStatus)
    {
        Status = newStatus;
        UpdateTime = DateTimeOffset.UtcNow;
    }

    public void UpdateRole(UserRole newRole)
    {
        UserRole = newRole;
        UpdateTime = DateTimeOffset.UtcNow;
    }
}
