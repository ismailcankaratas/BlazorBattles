﻿using BlazorBattles.Server.Data;
using BlazorBattles.Server.Services;
using BlazorBattles.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserUnitController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserUnitController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }
        [HttpPost("revive")]
        public async Task<IActionResult> ReviveArmy()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits
                .Where(unit => unit.UserId == user.Id)
                .Include(unit => unit.Unit)
                .ToListAsync();

            int bananaConst = 1000;

            if(user.Bananas < bananaConst)
            {
                return BadRequest("Yeterli muz yok! Ordunu canlandırmak için 1000 muza ihtiyacın var.");
            }
            bool armyAlreadyAlive = true;
            foreach (var userUnit in userUnits)
            {
                if(userUnit.HitPoints <= 0)
                {
                    armyAlreadyAlive = false;
                    userUnit.HitPoints = new Random().Next(0, userUnit.Unit.HitPoints);
                }
            }
            if (armyAlreadyAlive)
                return Ok("Ordun zaten hayatta");
            user.Bananas -= bananaConst;

            await _context.SaveChangesAsync();

            return Ok("Ordu canlandı!");
        }
        [HttpPost]
        public async Task<IActionResult> BuildUserUnit([FromBody] int unitId)
        {
            var unit = await _context.Units.FirstOrDefaultAsync<Unit>(u => u.Id == unitId);
            var user = await _utilityService.GetUser();
            if(user.Bananas < unit.BananaConst)
            {
                return BadRequest("Yeterli muz yok!");
            }

            user.Bananas -= unit.BananaConst;

            var newUserUnit = new UserUnit
            {
                UnitId = unit.Id,
                UserId = user.Id,
                HitPoints = unit.HitPoints
            };

            _context.UserUnits.Add(newUserUnit);
            await _context.SaveChangesAsync();
            return Ok(newUserUnit);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserUnits()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits.Where(unit => unit.UserId == user.Id).ToListAsync();

            var response = userUnits.Select(
                unit => new UserUnitResponse
                {
                    UnitId = unit.UnitId,
                    HitPoints = unit.HitPoints
                }
                );
            return Ok(response);
        }
    }
}
