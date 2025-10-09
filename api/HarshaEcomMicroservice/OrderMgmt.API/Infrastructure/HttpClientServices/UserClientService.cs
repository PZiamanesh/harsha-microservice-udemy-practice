using Microsoft.Extensions.Caching.Distributed;

namespace OrderMgmt.API.Infrastructure.HttpClientServices;

public class UserClientService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IDistributedCache _cache;

    public UserClientService(
        HttpClient httpClient,
        JsonSerializerOptions jsonSerializerOptions,
        IDistributedCache cache)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = jsonSerializerOptions;
        _cache = cache;
    }

    public async Task<UserResponse?> GetUserById(Guid userId)
    {
        var userRedis = await _cache.GetStringAsync($"user_{userId}");
        if (userRedis is not null)
        {
            return JsonSerializer.Deserialize<UserResponse>(userRedis);
        }

        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{userId}");
        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        using var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStreamAsync();
        var userResponse = await JsonSerializer.DeserializeAsync<UserResponse>(content, _jsonSerializerOptions);

        var userCache = JsonSerializer.Serialize(userResponse);
        await _cache.SetStringAsync($"user_ {userId}",
            userCache,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });

        return userResponse;
    }
}
