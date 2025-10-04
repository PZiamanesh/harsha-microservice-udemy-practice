namespace OrderMgmt.API.Infrastructure.HttpClientServices;

public class ProductClientService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ProductClientService(
        HttpClient httpClient,
        JsonSerializerOptions jsonSerializerOptions)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    public async Task<ProductResponse?> GetProductById(Guid productId)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{productId}");
        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        using var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStreamAsync();
        var productResponse = await JsonSerializer.DeserializeAsync<ProductResponse>(content, _jsonSerializerOptions);

        return productResponse;
    }
}
