using System.Text.RegularExpressions;

namespace AdventureAPI.Core.ValueObjects;

/// <summary>
///     Address information
/// </summary>
/// <param name="postalCode">XXX-XXXX</param>
/// <param name="prefecture">都道府県</param>
/// <param name="city">市</param>
/// <param name="ward">区</param>
/// <param name="block">丁目</param>
/// <param name="number">番地</param>
/// <param name="building">Optional field for building name and room number</param>
/// <param name="lng">Optional field for longitude</param>
/// <param name="lat">Optional field for latitude</param>
public class Address(
    string postalCode,
    string prefecture,
    string city,
    string ward,
    string block,
    string? number,
    string? building = "",
    double lng = 0D,
    double lat = 0D
) : ValueObject
{
    public string PostalCode { get; } = ValidatePostalCode(postalCode);
    public string Prefecture { get; } = Guard.Against.NullOrWhiteSpace(prefecture, nameof(prefecture));
    public string City { get; } = Guard.Against.NullOrWhiteSpace(city, nameof(city));
    public string Ward { get; } = Guard.Against.NullOrWhiteSpace(ward, nameof(ward));
    public string Block { get; } = Guard.Against.NullOrWhiteSpace(block, nameof(block));
    public string Number { get; } = number ?? string.Empty;
    public string Building { get; } = building ?? string.Empty;
    public double Lng { get; } = ValidateLongitude(lng);
    public double Lat { get; } = ValidateLongitude(lat);

    public Address UpdateCoordinates(double longitude, double latitude)
    {
        ValidateLongitude(lng);
        ValidateLongitude(lat);
        return new Address(PostalCode, Prefecture, City, Ward, Block, Number, Building, longitude, latitude);
    }

    public override string ToString()
    {
        var address = $"〒{PostalCode} {Prefecture}{City}{Ward}{Block}{Number}";
        if (!string.IsNullOrWhiteSpace(Building))
        {
            address += $" {Building}";
        }

        return address;
    }

    private static string ValidatePostalCode(string postalCode)
    {
        Guard.Against.NullOrWhiteSpace(postalCode, nameof(postalCode));

        // Japanese postal code format: XXX-XXXX
        var regex = new Regex(@"^\d{3}-\d{4}$");
        if (!regex.IsMatch(postalCode))
        {
            throw new ArgumentException("Invalid postal code format. Use XXX-XXXX format.", nameof(postalCode));
        }

        return postalCode;
    }

    private static double ValidateLongitude(double lng)
    {
        return Guard.Against.OutOfRange(lng, nameof(lng), -180, 180);
    }

    private static double ValidateLatitude(double lat)
    {
        return Guard.Against.OutOfRange(lat, nameof(lat), -90, 90);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PostalCode;
        yield return Prefecture;
        yield return City;
        yield return Ward;
        yield return Block;
        yield return Number;
        yield return Building;
        yield return Lng;
        yield return Lat;
    }
}
