using BlazorBattles.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBattles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        public IList<Unit> Units => new List<Unit>
        {
            new Unit { Id = 1, Title = "Knight", Attack = 10, Defanse = 10, BananaConst = 100 },
            new Unit { Id = 2, Title = "Archer", Attack = 15, Defanse = 5, BananaConst = 150 },
            new Unit { Id = 3, Title = "Mage", Attack = 20, Defanse = 1, BananaConst = 200 },
        };
        // async : Birimler alma yöntemini eşzamansız hale getirmek için
        // veritabanından veri getirdiğimizde, yalnızca eşzamansızı ekleriz.
        [HttpGet]
        public async Task<IActionResult> GetUnit()
        {
            return Ok(Units);
        }
    }
}
