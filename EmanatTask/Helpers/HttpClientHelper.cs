using EmanatTask.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmanatTask.Helpers
{
    public class HttpClientHelper
    {
        readonly OmdbApiConfiguration _omdbConfig;

        public HttpClientHelper(OmdbApiConfiguration omdbConfig)
        {
            _omdbConfig = omdbConfig;
        }

        public async Task<T> Send<T>(string query)
        {
            HttpClient client = new HttpClient();
            var requestUrl = new Uri(string.Format(_omdbConfig.URI, _omdbConfig.API_KEY) + query);
            var response = await client.GetAsync(requestUrl);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(jsonResponse);
            return result;
        }
    }
}
