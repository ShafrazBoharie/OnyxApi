using System.Net;
using FluentAssertions.Execution;
using Onyx.Api.IntegrationTests.Controllers;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Onyx.Models.Models;

public class ProductControllerIntegrationTests
{
    private readonly string _bearerToken;
    public ProductControllerIntegrationTests()
    {
        _bearerToken = GetBearerToken().Result;
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturn401_WhenNoTokenIsProvided()
    {
        var application = new ProductWebApplicationFactory();
        var _httpClient = application.CreateClient();
        var response = await _httpClient.GetAsync("/api/Products");

        using (new AssertionScope())
        {
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }

    [Fact]
    public async Task GetAllProducts_ReturnsOk()
    {
        var application = new ProductWebApplicationFactory();
        var _httpClient = application.CreateClient();
        _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _bearerToken);
        var response = await _httpClient.GetAsync("/api/Products");

        using (new AssertionScope())
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(content);
            products.Count.Should().BeGreaterThanOrEqualTo(1);

        }
    }

    [Fact]
    public async Task GetAllProductsByColor_ShouldReturn401_WhenNoTokenIsProvided()
    {
        var application = new ProductWebApplicationFactory();
        var _httpClient = application.CreateClient();
        var response = await _httpClient.GetAsync("/api/Products/white");

        using (new AssertionScope())
        {
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }

    [Fact]
    public async Task GetAllProductsByColor_ReturnsOk()
    {
        var application = new ProductWebApplicationFactory();
        var _httpClient = application.CreateClient();
        _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _bearerToken);
        var response = await _httpClient.GetAsync("/api/Products/white");

        using (new AssertionScope())
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(content);
            products.Count.Should().BeGreaterThanOrEqualTo(1);
        }
    }


    private async Task<string> GetBearerToken()
    {
        var httpClient = new HttpClient();
        var requestContent = new
        {
            client_id = "ipgc0ZFSmwiVjDiZ1aiq5iKSiQ7TF8vy",
            client_secret = "q1psLW2EWS58jvGvMOGOCg3CkmjKYpZvaFD0LYcv0hvtda94954TjCJ7uz2XRK9u",
            audience = "http://api.onyx.com",
            grant_type = "client_credentials"
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestContent), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("https://dev-52x28yi6qz7k4grd.us.auth0.com/oauth/token", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);

        return responseObject["access_token"].ToString();
    }

}
        