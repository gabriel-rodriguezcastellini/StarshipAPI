using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarshipAPI.Models;
using StarshipAPI.Services;

namespace StarshipAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StarshipsController(IStarWarsService starWarsService) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetStarships([FromQuery] string? manufacturer)
        {
            IEnumerable<Starship> starships = await starWarsService.GetStarshipsAsync();
            if (!string.IsNullOrEmpty(manufacturer))
            {
                starships = starships.Where(s => s.GetManufacturers().Contains(manufacturer));
            }
            return Ok(starships);
        }
    }
}
