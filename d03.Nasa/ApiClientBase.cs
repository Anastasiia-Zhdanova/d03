using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace d03.Nasa
{
    public abstract class ApiClientBase
    {
        protected string ApiKey { get; }

        protected ApiClientBase(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        protected async Task<T> HttpGetAsync<T>(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonSerializer.Deserialize<T>(jsonResponse);
            throw new HttpRequestException($"GET \"{url}\" returned {response.StatusCode}:\n{jsonResponse}");
        }
        
        
    }
}