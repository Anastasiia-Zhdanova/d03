using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using d03.Nasa.NeoWs.Models;

namespace d03.Nasa.NeoWs
{
    public class NeoWsClient : ApiClientBase, INasaClient<AsteroidRequest, Task<AsteroidLookup[]>>
    {
        private const string feedBaseUrl = "https://api.nasa.gov/neo/rest/v1/feed";
        private const string lookUpUrl = "https://api.nasa.gov/neo/rest/v1/neo/";

        private async Task<AsteroidLookup> LookupAsync(string id)
        {
            var uriBuilder = new UriBuilder(lookUpUrl + id + "/")
            {
                Query = $"api_key={ApiKey}"
            };
            return await HttpGetAsync<AsteroidLookup>(uriBuilder.Uri.ToString());
        }

        public class NearEarthClass
        {
            [JsonPropertyName("near_earth_objects")]
            public Dictionary<DateTime, AsteroidInfo[]> Objects { get; set; }
        }

        public async Task<AsteroidLookup[]> GetAsync(AsteroidRequest input)
        {
            var uriBuilder = new UriBuilder(feedBaseUrl)
            {
                Query = $"start_date={input.StartDate:yyyy-MM-dd}&end_date={input.EndDate:yyyy-MM-dd}&api_key={ApiKey}"
            };
            var feedResult = await HttpGetAsync<NearEarthClass>(uriBuilder.Uri.ToString());
            var lookupTasks = feedResult.Objects.
                SelectMany((entryPerem) => entryPerem.Value).
                OrderBy(asteroidInf=>asteroidInf.Distance).
                Select(asteroidInformation=>asteroidInformation.Id).
                Distinct().
                Take(input.ResultCount).
                Select(LookupAsync).
                ToList();
            return await Task.WhenAll(lookupTasks);
        }

        public NeoWsClient(string apiKey) : base(apiKey)
        {
        }
    }
}