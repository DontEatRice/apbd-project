using Microsoft.EntityFrameworkCore;
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

        public async Task<UserTicker?> GetUserTickerByNames(string tickerName, string userName)
        {
            return await GetByCondition(ut => ut.Ticker.TickerSymbol == tickerName && ut.User.NormalizedUserName == userName.ToUpper()).FirstOrDefaultAsync();
        }

        public async Task<List<UserTicker>> UserWatchlist(string userId)
        {
            return await GetByCondition(ut => ut.IdUser == userId).Include(e => e.Ticker).ToListAsync();
        }

    }
}