using System;
using System.Threading.Tasks;
using d03.Nasa.Apod.Models;

namespace d03.Nasa.Apod
{
    public class ApodClient : ApiClientBase, INasaClient<int, Task<MediaOfToday[]>>
    {
        private const string apodBaseUrl = "https://api.nasa.gov/planetary/apod";
        
        public ApodClient(string apiKey) : base(apiKey)
        {
        }

        public async Task<MediaOfToday[]> GetAsync(int input)
        {
            var uriBuilder = new UriBuilder(apodBaseUrl)
            {
                Query = $"api_key={ApiKey}&count={input}"
            };
            return await HttpGetAsync<MediaOfToday[]>(uriBuilder.Uri.ToString());
        }
    }
}