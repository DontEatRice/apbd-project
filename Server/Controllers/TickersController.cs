using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{name}")]
        public async Task<IActionResult> Info(string name) {
            try {
                return Ok(await _service.GetTickerByName(name));
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