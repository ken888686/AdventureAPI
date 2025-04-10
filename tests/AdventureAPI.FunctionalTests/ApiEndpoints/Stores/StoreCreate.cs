using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using AdventureAPI.Core.ValueObjects;
using AdventureAPI.Web.Controllers.Auth;
using AdventureAPI.Web.Controllers.Stores;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdventureAPI.FunctionalTests.ApiEndpoints.Stores;

[Collection("Sequential")]
public class StoreCreate(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private const string AdminUsername = "admin";
    private const string NormalUsername = "ken888686";
    private const string Password = "test";
    private readonly HttpClient _client = factory.CreateClient();

    private readonly IConfiguration _configuration =
        factory.Services.GetRequiredService<IConfiguration>();

    private readonly Fixture _fixture = new();

    [Fact]
    public async Task StoreCreate_WithValidParametersAndWithToken_CreatesStore()
    {
        // Arrange
        var token = await ProcessLoginAsync(AdminUsername, Password);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            token
        );

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
                35.686958
            )
        };

        // Act
        var createStoreResponse = await _client.PostAsJsonAsync("/stores", store);
        createStoreResponse.EnsureSuccessStatusCode();
        var resultString = await createStoreResponse.Content.ReadAsStringAsync();
        Assert.NotNull(resultString);
        var result = JsonConvert.DeserializeObject<CreateStoreResponse>(resultString);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(HttpStatusCode.OK, createStoreResponse.StatusCode);
        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        Assert.Equal(store.Name, result.Data.Name);
        Assert.Equivalent(store.Address, result.Data.Address);
    }

    [Fact]
    public async Task StoreCreate_WithValidParametersAndWithoutToken_Returns401Unauthorized()
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
                35.686958
            )
        };

        // Act
        var response = await _client.PostAsJsonAsync("/stores", store);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private async Task<string> ProcessLoginAsync(string username, string password)
    {
        var loginResponse = await _client.PostAsJsonAsync(
            "/auth/login",
            new { username, password });
        loginResponse.EnsureSuccessStatusCode();
        var jsonString = await loginResponse.Content.ReadAsStringAsync();
        Assert.NotNull(jsonString);
        var jsonObj = JsonConvert.DeserializeObject<LoginResponse>(jsonString);
        Assert.NotNull(jsonObj);
        Assert.NotNull(jsonObj.Data);
        return jsonObj.Data;
    }
}
