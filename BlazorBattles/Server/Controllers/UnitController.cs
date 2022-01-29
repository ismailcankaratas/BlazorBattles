using BlazorBattles.Server.Data;
using BlazorBattles.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly DataContext _context;

        public UnitController(DataContext context)
        {
            _context = context;
        }
        // async : Birimler alma yöntemini eşzamansız hale getirmek için
        // veritabanından veri getirdiğimizde, yalnızca eşzamansızı ekleriz.
        [HttpGet]
        public async Task<IActionResult> GetUnit()
        {
            var units = await _context.Units.ToListAsync();
            return Ok(units);
        }
        //[HttpPost("AddUnit")]
        [HttpPost]
        public async Task<IActionResult> AddUnit(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
            return Ok(await _context.Units.ToListAsync());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, Unit unit)
        {
            var dbUnit = await _context.Units.FirstOrDefaultAsync(u => u.Id == id);
            if (dbUnit == null)
            {
                return NotFound("Belirtilen Idye sahip birim mevcut değil.");
            }

            dbUnit.Title = unit.Title;
            dbUnit.Attack = unit.Attack;
            dbUnit.Defanse = unit.Defanse;
            dbUnit.BananaConst = unit.BananaConst;
            dbUnit.HitPoints = unit.HitPoints;

            await _context.SaveChangesAsync();

            return Ok(dbUnit);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var dbUnit = await _context.Units.FirstOrDefaultAsync(u => u.Id == id);
            if (dbUnit == null)
            {
                return NotFound("Belirtilen Idye sahip birim mevcut değil.");
            }

            _context.Units.Remove(dbUnit);
            await _context.SaveChangesAsync();

            return Ok(await _context.Units.ToListAsync());
        }
    }
}
