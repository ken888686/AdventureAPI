using AutoFixture;

namespace AdventureAPI.FunctionalTests.ApiEndpoints.Stores;

[Collection("Sequential")]
public class StoreGetByName(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    private readonly Fixture _fixture = new();
    /*
    [Fact]
    public async Task StoreGetByName_WithValidId_RetrievesStore()
    {
        // Arrange
        const string storeName = "Store 100";

        // Act
        var response = await _client.GetAsync(
            GetStoreByNameRequest.BuildRoute(storeName));
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<GetStoreByNameResponse>(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(storeName, result.Data.Name);
    }
    */
}
