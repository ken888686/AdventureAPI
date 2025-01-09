namespace AdventureAPI.Core.Enums;

public class StoreStatus(string name, int value) : SmartEnum<StoreStatus>(name, value)
{
    public static readonly StoreStatus Active = new(nameof(Active), 1);
    public static readonly StoreStatus Inactive = new(nameof(Inactive), 2);
    public static readonly StoreStatus Pending = new(nameof(Pending), 3);
}
