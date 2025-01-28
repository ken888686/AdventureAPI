using System.Net;
using System.Net.Http.Headers;
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

    private readonly string _token =
        "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxLyIsImlzcyI6IkZ1bmN0aW9uYWxUZXN0cyIsImV4cCI6MTczNzQzNTQ1NCwiaWF0IjoxNzM3NDM1NDU0LCJVc2VyTmFtZSI6Imtlbjg4ODY4NiIsIlVzZXJJZCI6IjAxOTQ4NzM5LTE3NmMtNzBhMy1iZGJhLTAwN2Y2NzFlMTlkNCIsInJvbGUiOiJVc2VyIiwibmJmIjoxNzM3NDM1NDU0fQ.I_ADNjKlDmv_FazOMZnxzzjaP3NjQvZT-k_bvnLZwms";

    [Fact]
    public async Task StoreCreate_WithValidParametersAndWithToken_CreatesStore()
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
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        // Act
        var response = await _client.PostAsJsonAsync("/stores", store);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateStoreResponse>(jsonString);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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
                35.686958)
        };

        // Act
        var response = await _client.PostAsJsonAsync("/stores", store);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
