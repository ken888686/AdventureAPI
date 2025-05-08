using AdventureAPI.Core.Aggregates.CityAggregate;

namespace AdventureAPI.UnitTests.Core.CityAggregate;

public class CityConstructor
{
    private readonly string _testName = "test name";
    private City? _testCity;

    public City CreateCity()
    {
        return new City(_testName, "test user");
    }

    [Fact]
    public void InitializesName()
    {
        _testCity = CreateCity();
        Assert.Equal(_testName, _testCity.Name);
    }
}
