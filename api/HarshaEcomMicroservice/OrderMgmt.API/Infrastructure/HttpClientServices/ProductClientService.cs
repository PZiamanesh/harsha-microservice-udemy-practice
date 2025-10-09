using Microsoft.Extensions.Caching.Distributed;

namespace OrderMgmt.API.Infrastructure.HttpClientServices;

public class ProductClientService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IDistributedCache _cache;

    public ProductClientService(
        HttpClient httpClient,
        JsonSerializerOptions jsonSerializerOptions,
        IDistributedCache distributedCache)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = jsonSerializerOptions;
        _cache = distributedCache;
    }

    public async Task<ProductResponse?> GetProductById(Guid productId)
    {
        var productRedis = await _cache.GetStringAsync($"product_{productId}");
        if (productRedis is not null)
        {
            return JsonSerializer.Deserialize<ProductResponse>(productRedis);
        }

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

        var productCache = JsonSerializer.Serialize(productResponse);
        await _cache.SetStringAsync($"product_{productId}", 
            productCache, 
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });

        return productResponse;
    }
}
