using System.Transactions;
using blazor_project.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _watchlistService;
        private readonly ITickerService _tickerService;
        private readonly UserManager<ApplicationUser> _userManager;
        public WatchlistController(IWatchlistService watchlistService, ITickerService tickerService, UserManager<ApplicationUser> userManager)
        {
            _watchlistService = watchlistService;
            _tickerService = tickerService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user is null)
                return BadRequest("User does not exist");
            
            var watchlist = await _watchlistService.UserWatchlist(user.Id);
            var result = new Watchlist{
                Tickers = watchlist.Select(e => new blazor_project.Shared.Models.Ticker {
                    IdTicker = e.IdTicker,
                    TickerSymbol = e.Ticker.TickerSymbol
                })
            };
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromBody] WatchlistPOST body)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is invalid!");

            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user is null)
                return BadRequest("User does not exist");

            var ticker = await _tickerService.GetTickerByName(body.TickerSymbol);
            if (ticker is null)
                return BadRequest("Cannot find ticker in database");

            var userTicker = new UserTicker
            {
                IdTicker = ticker.IdTicker,
                IdUser = user.Id
            };
            try
            {
                _watchlistService.DeleteEntry(userTicker);
                await _watchlistService.SaveDatabaseAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem(e.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WatchlistPOST body)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is invalid!");

            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user is null)
                return BadRequest("User does not exist");

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var ticker = await _tickerService.GetTickerByName(body.TickerSymbol);
                    if (ticker is null)
                    {
                        ticker = new Ticker
                        {
                            TickerSymbol = body.TickerSymbol
                        };
                        _tickerService.CreateTicker(ticker);
                        await _tickerService.SaveDatabaseAsync();
                    }

                    if (await _watchlistService.GetUserTickerByNames(ticker.TickerSymbol, user.UserName) is not null)
                        return BadRequest("User is already following this ticker");

                    var userTicker = new UserTicker
                    {
                        IdTicker = ticker.IdTicker,
                        IdUser = user.Id
                    };
                    _watchlistService.CreateEntry(userTicker);

                    scope.Complete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Problem("Unexpected server error - " + e.Message);
                }
            }
            await _watchlistService.SaveDatabaseAsync();
            await _tickerService.SaveDatabaseAsync();

            return NoContent();
        }
    }
}