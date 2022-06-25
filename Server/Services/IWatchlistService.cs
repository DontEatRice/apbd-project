using Server.Models;

namespace Server.Services
{
    public interface IWatchlistService
    {
        void CreateEntry(UserTicker entry);
        void DeleteEntry(UserTicker entry);
        Task SaveDatabaseAsync();
        Task<List<UserTicker>> UserWatchlist(string userId);
        Task<UserTicker?> GetUserTickerByNames(string tickerName, string userName);
    }
}