using Service;
using Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;


[ApiController]
[Route("[Controller]")]
public class BattleController : ControllerBase
{ 

    private readonly IBattleService _battleService;

    public BattleController(IBattleService battleService)
    {
        _battleService = battleService;
    }

    [HttpPost ("createBattle/{pokemonId1}/{pokemonId2}/{moves1string}/{moves2string}")]
    public IActionResult CreateBattle(int pokemonId1, int pokemonId2, string moves1string, string moves2string)
    {

        List<string> moves1 = new List<string>(moves1string.Split(","));
        List<string> moves2 = new List<string>(moves2string.Split(","));

        // Create a battle instance
        Battle battle = new Battle
        {
            PokemonId1 = pokemonId1,
            P1StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
            PokemonId2 = pokemonId2,
            P2StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
            BattleWinner = Battle.Winner.NotFinished,
            BattleStatus = Battle.Status.InProgress,
            battlePhase = Battle.BattlePhase.Selection,
            P1Moves = moves1,
            P2Moves = moves2
        };

        // Normalize the pokemon to level 50 stats
        // TODO: Implement the logic to normalize the pokemon
        battle = _battleService.NormalizePokemon(battle);

        // Persist the battle
        _battleService.CreateBattle(battle);

        return Ok(battle.BattleId);
    }

    [HttpPut ("updateBattle/{battleId}/{pokemon1Move}/{pokemon2Move}")]
    public async Task<IActionResult> UpdateBattle(int battleId, string pokemon1Move, string pokemon2Move)
    {
        // They'll send us attacking pokemon with their move choice for the round. (Put Endpoint)
        // We'll pass back string builder with attack order/priority, hit/miss, remaining hp and update hp on our end. (Result of the Put Endpoint)
        
        // Get the battle from the database
        Battle battle = _battleService.GetBattle(battleId);

        // Update the battle with the results of using the selected moves as they impact pokemon health.
        int firstToMove = _battleService.CalculatePriority(battle, pokemon1Move, pokemon2Move);
        bool move1Hit = _battleService.CalculateHit(battle, pokemon1Move);
        bool move2Hit = _battleService.CalculateHit(battle, pokemon2Move);
        Task<string> damageResult = _battleService.CalculateDamage(battle, pokemon1Move, pokemon2Move);
        string returnInfo = _battleService.UpdateBattle(battle, firstToMove, move1Hit, move2Hit, damageResult);

        return Ok(returnInfo);
    }

    [HttpDelete ("deleteBattle/{battleId}")]
    public IActionResult DeleteBattle(int battleId)
    {
        _battleService.DeleteBattle(battleId);
        return Ok();
    }

    [HttpGet ("getBattle/{battleId}")]
    public IActionResult GetBattle(int battleId)
    {
        var battle = _battleService.GetBattle(battleId);
        return Ok(JsonSerializer.Serialize(battle));
    }

    [HttpGet ("getBattles")]
    public IActionResult GetBattles()
    {
        var battles = _battleService.GetBattles();
        return Ok(JsonSerializer.Serialize(battles));
    }
}

    //TODO: 
        // They'll pass us battleid, two pokemon, and two move lists. (Post Endpoint)
        // we'll create a battle instance, normalize the pokemon to lvl 50 stats, and persist it. (Result of the Post Endpoint)
        // They'll send us attacking pokemon with their move choice for the round. (Put Endpoint)
        // We'll pass back string builder with attack order/priority, hit/miss, remaining hp and update hp on our end. (Result of the Put Endpoint)
        // They'll let us know if the battle is over so we can remove the battle from the database. (Delete Endpoint)

    // }