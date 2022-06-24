using Server.Models;

namespace Server.Services
{
    public interface IWatchlistService
    {
        void CreateEntry(UserTicker entry);
        void DeleteEntry(UserTicker entry);
        Task SaveDatabaseAsync();
        IQueryable<UserTicker> UserWatchlist(string userId);
        IQueryable<UserTicker> GetUserTickerByNames(string tickerName, string userName);
    }
}