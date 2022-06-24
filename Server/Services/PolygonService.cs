using Newtonsoft.Json;
using Server.Data;
using Server.Models;
using Server.Models.PolygonModels;

namespace Server.Services
{
    public class PolygonService : IPolygonService
    {
        private readonly HttpClient _httpClient;
        private readonly string API_KEY = "IJnGSdPmO_DthedPPJIYRdBh6WKCZp97";
        private readonly string BASE_URL = "https://api.polygon.io/v3/reference/";

        public PolygonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
 

        }

        public async Task<TickerInfo?> GetTickerByName(string name)
        {
            var response = await _httpClient.GetStringAsync($"{BASE_URL}tickers/{name.ToUpper()}?apiKey={API_KEY}");
            return JsonConvert.DeserializeObject<TickerInfo>(response);
        }


        public async Task<Tickers?> Search(string term)
        {
            var response = await _httpClient.GetStringAsync( $"{BASE_URL}tickers?search={term.ToUpper()}&market=stocks&active=true&sort=ticker&limit=20&order=asc&apiKey={API_KEY}");
            return JsonConvert.DeserializeObject<Tickers>(response);
        }

    }
}