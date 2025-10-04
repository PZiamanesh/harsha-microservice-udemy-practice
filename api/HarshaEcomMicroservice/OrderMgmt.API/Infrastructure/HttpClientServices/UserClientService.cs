namespace OrderMgmt.API.Infrastructure.HttpClientServices;

public class UserClientService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public UserClientService(
        HttpClient httpClient,
        JsonSerializerOptions jsonSerializerOptions)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    public async Task<UserResponse?> GetUserById(Guid userId)
    {
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

        return userResponse;
    }
}
