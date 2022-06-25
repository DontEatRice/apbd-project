using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Services
{
    public class TickerService : ServiceBase<Ticker>, ITickerService
    {
        private readonly ApplicationDBContext _context;
        public TickerService(ApplicationDBContext context) : base(context) {
            _context = context;
        }
        public void CreateTicker(Ticker ticker)
        {
            Create(ticker);
        }

        public async Task<Ticker?> GetTickerByName(string name)
        {
            return await GetByCondition(t => t.TickerSymbol == name).IgnoreAutoIncludes().FirstOrDefaultAsync();
        }
    }
}