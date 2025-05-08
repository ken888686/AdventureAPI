using System.Net;
using System.Net.Http.Json;
using AdventureAPI.Web.Controllers.Auth;
using AutoFixture;
using Newtonsoft.Json;

namespace AdventureAPI.FunctionalTests.ApiEndpoints.Auth;

[Collection("Sequential")]
public class AuthLogin(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task AuthLogin_WithValidUsernameAndPassword_ReceivesToken()
    {
        // Arrange
        const string username = "ken888686";
        const string password = "test";

        // Act
        var response = await _client.PostAsJsonAsync(
            "/auth/login",
            new { username, password });
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<LoginResponse>(jsonString);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
    }

    [Fact]
    public async Task AuthLogin_WithInvalidUsernameAndValidPassword_ThrowsNotFound()
    {
        // Arrange
        const string username = "nobody";
        const string password = "test";

        // Act
        var response = await _client.PostAsJsonAsync("/auth/login", new { username, password });
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<LoginResponse>(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
    }

    [Fact]
    public async Task AuthLogin_WithValidUsernameAndInvalidPassword_ThrowsError()
    {
        // Arrange
        const string username = "ken888686";
        const string password = "nope";

        // Act
        var response = await _client.PostAsJsonAsync("/auth/login", new { username, password });
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<LoginResponse>(jsonString);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        Assert.NotNull(result.Messages);
        Assert.Equal("Password is incorrect.", result.Messages.FirstOrDefault());
    }
}
