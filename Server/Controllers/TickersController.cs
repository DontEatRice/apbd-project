using blazor_project.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    // [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TickersController : ControllerBase
    {
        private readonly IPolygonService _service;
        public TickersController(IPolygonService service) {
            _service = service;
        }

        [HttpGet("{name}/{from}")]
        public Task<IActionResult> Price(string name, long from) {
            return Price(name, from, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() * 1000);
        }

        [HttpGet("{name}/{from}/{to}")]
        public async Task<IActionResult> Price(string name, long from, long to) {
            try {
                var data = await _service.GetPrice(name, from, to);
                if (data is null)
                    return NoContent();
                return Ok(data.results.Select(e => new TickerPrice{
                    open = e.o,
                    high = e.h,
                    low = e.l,
                    close = e.c,
                    time = DateTimeOffset.FromUnixTimeMilliseconds(e.t).DateTime,
                    volume = e.v
                }));
            } catch (Exception e) {
                return Problem(e.Message);
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Info(string name) {
            try {
                var result = await _service.GetTickerByName(name);
                if (result is null)
                    return NotFound();

                result.results.branding = new Models.PolygonModels.Branding {
                    icon_url = result.results.branding.icon_url + "?apiKey=" + _service.GetApiKey(),
                    logo_url = result.results.branding.logo_url + "?apiKey=" + _service.GetApiKey()
                };
                return Ok(result);
            } catch (Exception e) {
                Console.WriteLine(e);
                return Problem(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery(Name = "q")] string term) {
            try {
                return Ok(
                    (await _service.Search(term))?.results.Select(e => new blazor_project.Shared.Models.DTOs.TickerSearch {TickerSymbol = e.ticker, Name = e.name})
                );
            } catch (Exception e) {
                Console.WriteLine(e);
                return Problem(e.Message);
            }
        }
    }
}