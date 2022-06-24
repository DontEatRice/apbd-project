using Server.Data;
using Server.Models;

namespace Server.Services
{
    public class WatchlistService : ServiceBase<UserTicker>, IWatchlistService
    {
        private readonly ApplicationDBContext _context;
        public WatchlistService(ApplicationDBContext context) : base(context) {
            _context = context;
        }

        public void CreateEntry(UserTicker entry)
        {
            Create(entry);
        }

        public void DeleteEntry(UserTicker entry)
        {
            Delete(entry);
        }

        public IQueryable<UserTicker> UserWatchlist(string userId)
        {
            return GetByCondition(ut => ut.User.Id == userId);
        }

        public IQueryable<UserTicker> GetUserTickerByNames(string tickerName, string userName) {
            return GetByCondition(ut => ut.Ticker.TickerSymbol == tickerName && ut.User.NormalizedUserName == userName.ToUpper());
        }
    }
}