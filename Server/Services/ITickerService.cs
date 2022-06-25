using Server.Models;

namespace Server.Services
{
    public interface ITickerService
    {
        void CreateTicker(Ticker ticker);
        Task<Ticker?> GetTickerByName(string name);
        Task SaveDatabaseAsync();
    }
}