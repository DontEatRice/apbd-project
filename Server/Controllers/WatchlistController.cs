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
        private readonly IPolygonService _polygonService;
        private readonly UserManager<ApplicationUser> _userManager;
        public WatchlistController(IWatchlistService watchlistService, ITickerService tickerService, IPolygonService polygonService, UserManager<ApplicationUser> userManager)
        {
            _watchlistService = watchlistService;
            _tickerService = tickerService;
            _userManager = userManager;
            _polygonService = polygonService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user is null)
                return BadRequest("User does not exist");
            
            var watchlist = await _watchlistService.UserWatchlist(user.Id);
            return Ok(watchlist.Select(e => new blazor_project.Shared.Models.Ticker {
                IdTicker = e.IdTicker,
                TickerSymbol = e.Ticker.TickerSymbol,
                LogoUrl = e.Ticker.LogoUrl + "?apiKey=" + _polygonService.GetApiKey(),
                Name = e.Ticker.Name,
                Sic = e.Ticker.Sic
            }));
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Remove(string name)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is invalid!");

            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user is null)
                return BadRequest("User does not exist");

            var ticker = await _watchlistService.GetUserTickerByNames(name, user.UserName);
            if (ticker is null) {
                return NotFound();
            }

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

            try
            {
                var ticker = await _tickerService.GetTickerByName(body.TickerSymbol);
                if (ticker is null)
                {
                    var tickerInfo = await _polygonService.GetTickerByName(body.TickerSymbol);
                    ticker = new Ticker
                    {
                        TickerSymbol = body.TickerSymbol,
                        LogoUrl = tickerInfo?.results.branding?.logo_url,
                        Name = tickerInfo?.results.name,
                        Sic = tickerInfo?.results.sic_description
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
                await _watchlistService.SaveDatabaseAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Problem("Unexpected server error - " + e.Message);
            }

            return NoContent();
        }
    }
}