using System.Net;
using System.Net.Http.Json;
using AdventureAPI.Core.ValueObjects;
using AdventureAPI.Web.Controllers.Stores;
using AutoFixture;
using Newtonsoft.Json;

namespace AdventureAPI.FunctionalTests.ApiEndpoints.Stores;

[Collection("Sequential")]
public class StoreCreate(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task StoreCreate_WithValidParameters_CreatesStore()
    {
        // Arrange
        var store = new CreateStoreRequest
        {
            Name = _fixture.Create<string>(),
            Address = new Address(
                "100-0005",
                "Tokyo",
                "Chiyoda",
                "Marunouchi",
                "1 Chome",
                "",
                "",
                139.749466,
                35.686958)
        };

        // Act
        var response = await _client.PostAsJsonAsync("/stores", store);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateStoreResponse>(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(store.Name, result.Data.Name);
        Assert.Equivalent(store.Address, result.Data.Address);
    }
}
