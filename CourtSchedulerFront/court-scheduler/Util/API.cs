using System.Net.Http;
using System.Text.Json;

public class API {

    public async Task<T> Get<T>(HttpClient client, Uri endpoint)
    {
        var res = await client.GetAsync(endpoint);
        res.EnsureSuccessStatusCode();

        string responseBody = await res.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return result;
    }

    public async Task<T> Get<T>(Uri endpoint)
    {
        using var client = new HttpClient();
        return await Get<T>(client, endpoint);
    }

}