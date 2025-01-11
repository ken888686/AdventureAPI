using System.Net;
using AdventureAPI.Infrastructure.Data;
using AdventureAPI.Web.Controllers.Cities;
using Newtonsoft.Json;

namespace AdventureAPI.FunctionalTests.ApiEndpoints.Cities;

public class CityList(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CitiesList_WithoutInput_ReturnsTwoCitiesWithOkResponse()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/cities");
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CityListResponse>(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Data.Count());
        Assert.Contains(result.Data, i => i.Name == SeedData.City1.Name);
        Assert.Contains(result.Data, i => i.Name == SeedData.City2.Name);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }
}
