using System.Net;
using AdventureAPI.Infrastructure.Data;
using AdventureAPI.Web.Controllers.Stores;
using Newtonsoft.Json;

namespace AdventureAPI.FunctionalTests.ApiEndpoints.Stores;

[Collection("Sequential")]
public class StoreList(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task StoresList_WithoutInput_ReturnsTwoStoresWithOkResponse()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/stores");
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<StoreListResponse>(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(2, result.Data.Count());
        Assert.Contains(result.Data, i => i.Name == SeedData.Store1.Name);
        Assert.Contains(result.Data, i => i.Name == SeedData.Store2.Name);
    }
}
