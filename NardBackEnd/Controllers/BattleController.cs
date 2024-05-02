using Service;
using Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text;

namespace Controllers;
[ApiController]
[Route("[Controller]")]
public class BattleController : ControllerBase
{ 

    private readonly IBattleService _battleService;
    private readonly IPokemonService _pokemonService;

    public BattleController(IBattleService battleService, IPokemonService pokemonService)
    {
        _battleService = battleService;
        _pokemonService = pokemonService;
    }

    [HttpPost ("createBattle/{pokemon1Name}/{pokemonId2}/{moves1string}/{moves2string}")]
    public async Task<IActionResult> CreateBattle(string pokemon1Name, int pokemonId2, string moves1string, string moves2string)
    {

        List<string> moves1 = new List<string>(moves1string.Split(","));
        List<string> moves2 = new List<string>(moves2string.Split(","));

        Pokemon pokemon1 = await _pokemonService.SearchPokemon(pokemon1Name);

        // Create a battle instance
        Battle battle = new Battle
        {
            PokemonId1 = pokemon1.Id,
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
        //create a string builder and loop through the p1statblock to create a string of stats
        StringBuilder sb = new StringBuilder();
        foreach (string stat in battle.P1StatBlock)
        {
            sb.Append(stat);
            sb.Append(", ");
        }
        StringBuilder sb2 = new StringBuilder();
        foreach (string stat in battle.P2StatBlock)
        {
            sb2.Append(stat);
            sb2.Append(", ");
        }
        sb.Remove(sb.Length - 2, 2);
        sb2.Remove(sb2.Length - 2, 2);
        string sbString = sb.ToString();
        string sbString2 = sb2.ToString();
        string concatInfo = $"Battle id {battle.BattleId}, Pokemon 1 stat block is {sbString}, Pokemon 2 stat block is {sbString2}";

        return Ok(concatInfo);
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

