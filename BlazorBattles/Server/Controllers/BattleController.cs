using BlazorBattles.Server.Data;
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
    public class BattleController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public BattleController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }
        [HttpPost]
        public async Task<IActionResult> StartBattle([FromBody] int opponentId)
        {
            var attacker = await _utilityService.GetUser();
            var opponent = await _context.Users.FindAsync(opponentId);

            if(opponent == null || opponent.IsDeleted)
            {
                return NotFound("Rakip mevcut değil");
            }
            var result = new BattleResult();
            await Fight(attacker, opponent, result);

            return Ok(result);
        }

        private async Task Fight(User attacker, User opponent, BattleResult result)
        {
            var attackerArmy = await _context.UserUnits
                .Where(u => u.UserId == attacker.Id && u.HitPoints > 0)
                .Include(u => u.Unit)
                .ToListAsync();
            var opponentArmy = await _context.UserUnits
                .Where(u => u.UserId == opponent.Id && u.HitPoints > 0)
                .Include(u => u.Unit)
                .ToListAsync();
            var attackerDamgeSum = 0;
            var opponentDamgeSum = 0;

            int currentRound = 0;
            while(attackerArmy.Count > 0 && opponentArmy.Count > 0 )
            {
                currentRound++;

                if (currentRound % 2 != 0)
                    attackerDamgeSum += FightRound(attacker, opponent, attackerArmy, opponentArmy, result);
                else
                    opponentDamgeSum += FightRound(opponent, attacker, opponentArmy, attackerArmy, result);
            }
            result.IsVictory = opponentArmy.Count == 0;
            result.RoundsFought = currentRound;

            if (result.RoundsFought > 0)
                await FinishFight(attacker, opponent, result, attackerDamgeSum, opponentDamgeSum);
        }

        private int FightRound(User attacker, User opponent, List<UserUnit> attackerArmy, List<UserUnit> opponentArmy, BattleResult result)
        {
            int randomAttackerIndex = new Random().Next(attackerArmy.Count);
            int randomOpponentIndex = new Random().Next(opponentArmy.Count);

            var randomAttacker = attackerArmy[randomAttackerIndex];
            var randomOpponent = opponentArmy[randomOpponentIndex];

            var damage =
                new Random().Next(randomAttacker.Unit.Attack) - new Random().Next(randomOpponent.Unit.Defanse);
            if (damage < 0) damage = 0;
            if(damage <= randomOpponent.HitPoints)
            {
                randomOpponent.HitPoints -= damage;
                result.Log.Add(
                    $"{attacker.Username}'ın {randomAttacker.Unit.Title}, " +
                    $"{opponent.Username}'s {randomOpponent.Unit.Title} {damage} hasarla saldırdı." 
                    );
                return damage;
            }
            else
            {
                damage = randomOpponent.HitPoints;
                randomOpponent.HitPoints = 0;
                opponentArmy.Remove(randomOpponent);
                result.Log.Add(
                    $"{attacker.Username}'ın {randomAttacker.Unit.Title}'ı, " +
                    $"{opponent.Username}'ın {randomOpponent.Unit.Title}'ını öldürdü!"
                    );
                return damage;
            }
        }
        private async Task FinishFight(User attacker, User opponent, BattleResult result, int attackerDamgeSum, int opponentDamgeSum)
        {
            result.AttackerDamgerSum = attackerDamgeSum;
            result.OpponentDamgerSum = opponentDamgeSum;

            attacker.Battels++;
            opponent.Battels++;
            if (result.IsVictory)
            {
                attacker.Victories++;
                opponent.Deffeats++;
                attacker.Bananas += opponentDamgeSum;
                opponent.Bananas += attackerDamgeSum * 5;
            }
            else
            {
                attacker.Deffeats++;
                opponent.Victories++;
                attacker.Bananas += attackerDamgeSum * 5;
                opponent.Bananas += opponentDamgeSum;
            }
            StoreBattleHistory(attacker, opponent, result);
            await _context.SaveChangesAsync();
        }

        private void StoreBattleHistory(User attacker, User opponent, BattleResult result)
        {
            var battle = new Battle();
            battle.Attacker = attacker;
            battle.Opponent = opponent;
            battle.RoundFought = result.RoundsFought;
            battle.WinnerDamage = result.IsVictory ? result.AttackerDamgerSum : result.OpponentDamgerSum;
            battle.Winner = result.IsVictory ? attacker : opponent;

            _context.Battles.Add(battle);
        }
    }
}
