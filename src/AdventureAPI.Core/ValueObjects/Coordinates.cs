namespace AdventureAPI.Core.ValueObjects;

public class Coordinates(double latitude, double longitude) : ValueObject
{
    public double Latitude { get; } = ValidateLongitude(latitude);
    public double Longitude { get; } = ValidateLatitude(longitude);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }

    private static double ValidateLongitude(double lng)
    {
        return Guard.Against.OutOfRange(lng, nameof(lng), -180, 180);
    }

    private static double ValidateLatitude(double lat)
    {
        return Guard.Against.OutOfRange(lat, nameof(lat), -90, 90);
    }
}
