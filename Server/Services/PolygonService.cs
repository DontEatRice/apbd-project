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

        public string GetApiKey()
        {
            return API_KEY;
        }

        public async Task<TickerPrice?> GetPrice(string name, long from, long to)
        {
            return await _httpClient.GetFromJsonAsync<TickerPrice>(
                $"https://api.polygon.io/v2/aggs/ticker/{name}/range/1/hour/{from}/{to}?adjusted=true&limit=50000&sort=asc&apiKey={API_KEY}"
            );
        }

        public async Task<TickerNews?> GetRecentNews(string symbol)
        {
            return await _httpClient.GetFromJsonAsync<TickerNews>(
                $"https://api.polygon.io/v2/reference/news?ticker={symbol}&limit=5&sort=published_utc&apiKey={API_KEY}"
            );
        }

        public async Task<TickerInfo?> GetTickerByName(string name)
        {
            return await _httpClient.GetFromJsonAsync<TickerInfo>($"{BASE_URL}tickers/{name.ToUpper()}?apiKey={API_KEY}");
        }


        public async Task<Tickers?> Search(string term)
        {
            return await _httpClient.GetFromJsonAsync<Tickers>( $"{BASE_URL}tickers?search={term.ToUpper()}&market=stocks&active=true&sort=ticker&limit=20&order=asc&apiKey={API_KEY}");
        }

    }
}