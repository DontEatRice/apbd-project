using blazor_project.Shared;
using blazor_project.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TickersController : ControllerBase
    {
        private readonly IPolygonService _service;
        public TickersController(IPolygonService service) {
            _service = service;
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> News(string name) {
            try {
                var data = await _service.GetRecentNews(name);
                if (data is null)
                    throw new Exception("no news?");
                var result = data.results.Select(e =>
                    new TickerNews {
                        article_url = e.article_url,
                        author = e.author,
                        description = e.description,
                        title = e.title,
                        published_utc = e.published_utc
                    }
                );
                return Ok(result);
            }catch (Exception e) {
                Console.WriteLine(e);
                return Problem(e.Message);
            }
        }

        [HttpGet("{name}/{from}")]
        public Task<IActionResult> Price(string name, long from) {
            return Price(name, from, Timestamp.ToTimestamp(DateTime.UtcNow));
        }

        [HttpGet("{name}/{from}/{to}")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> Price(string name, long from, long to) {
            try {
                var data = await _service.GetPrice(name, from, to);
                if (data is null)
                    return NoContent();

                if (data.results is null) {
                    return Ok(new List<object>());
                }
                return Ok(data.results?.Select(e => new TickerPrice{
                    open = e.o,
                    high = e.h,
                    low = e.l,
                    close = e.c,
                    time = Timestamp.FromTimestamp(e.t),
                    volume = e.v
                }));
            } catch (Exception e) {
                Console.WriteLine(e);
                return Problem(e.Message);
            }
        }

        [HttpGet("{name}")]
        [ResponseCache(Duration = 24*60)]
        public async Task<IActionResult> Info(string name) {
            try {
                var data = await _service.GetTickerByName(name);
                if (data is null)
                    return NotFound();

                var result = data.results;

                if (result.branding is not null) {
                    result.branding = new Models.PolygonModels.Branding {
                        icon_url = result.branding.icon_url + "?apiKey=" + _service.GetApiKey(),
                        logo_url = result.branding.logo_url + "?apiKey=" + _service.GetApiKey()
                    };
                }
                return Ok(result);
            } catch (Exception e) {
                Console.WriteLine(e);
                return Problem(e.Message);
            }
        }

        [HttpGet]
        [ResponseCache(Duration = 7*24*60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] {"q"})]
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