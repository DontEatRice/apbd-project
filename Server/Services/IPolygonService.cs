using Server.Models;
using Server.Models.PolygonModels;

namespace Server.Services
{
    public interface IPolygonService
    {
        public Task<TickerInfo?> GetTickerByName(string name);
        public Task<Tickers?> Search(string term);

        public string GetApiKey();
        public Task<TickerPrice?> GetPrice(string name, long from, long to);
    }
}