using Models;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;



namespace Service;

public class BattleService:IBattleService
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;
    private readonly IPokemonService _pokemonService;
    Dictionary<string, int> p1Stats = new Dictionary<string, int>(){ {"hp", 0}, {"atk", 0}, {"def", 0}, {"satk", 0}, {"sdef", 0}, {"spd", 0}};
    Dictionary<string, int> p2Stats = new Dictionary<string, int>(){ {"hp", 0}, {"atk", 0}, {"def", 0}, {"satk", 0}, {"sdef", 0}, {"spd", 0}};


    public BattleService(ApplicationDbContext context, IPokemonService pokemonService, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _context = context;
        _pokemonService = pokemonService;
    }

    public void CreateBattle(Battle battle)
    {
        _context.Battles.Add(battle);
        _context.SaveChanges();
    }

    public void UpdateBattle(Battle battle)
    {
        _context.Battles.Update(battle);
        _context.SaveChanges();
    }

    public string UpdateBattle (Battle battle, int firstToMove, bool move1Hit, bool move2Hit, Task<string> damageResult)
    {
        //convert move1Hit and move2Hit to strings
        string move1HitString = move1Hit ? "hit" : "miss";
        string move2HitString = move2Hit ? "hit" : "miss";

        battle.P1StatBlock = new List<string> { $"hp:{p1Stats["hp"]}", $"atk:{p1Stats["atk"]}", $"def:{p1Stats["def"]}", 
        $"satk:{p1Stats["satk"]}", $"sdef:{p1Stats["sdef"]}", $"spd:{p1Stats["spd"]}" };
        battle.P2StatBlock = new List<string> { $"hp:{p2Stats["hp"]}", $"atk:{p2Stats["atk"]}", $"def:{p2Stats["def"]}", 
        $"satk:{p2Stats["satk"]}", $"sdef:{p2Stats["sdef"]}", $"spd:{p2Stats["spd"]}" };

        // build a string with the results of the round
        string roundResult = $"{firstToMove}" +  move1HitString + move2HitString + damageResult.Result;
        Task<string> _roundResult = Task.FromResult(roundResult);

        _context.Battles.Update(battle);
        _context.SaveChanges();

        return _roundResult.Result;
    }

    public void DeleteBattle(int battleId)
    {
        var battle = _context.Battles.Find(battleId);
        _context.Battles.Remove(battle);
        _context.SaveChanges();
    }

    public Battle GetBattle(int battleId)
    {
        return _context.Battles.Find(battleId);
    }

    public List<Battle> GetBattles()
    {
        return _context.Battles.ToList();
    }

    public Battle NormalizePokemon(Battle battle)
    {
        Pokemon baseP1 = _context.Pokemon.Find(battle.PokemonId1);
        List<int> statsList1 = new List<int>(){baseP1.Hp, baseP1.Atk, baseP1.Def, baseP1.Satk, baseP1.Sdef, baseP1.Spd};
        Pokemon baseP2 = _context.Pokemon.Find(battle.PokemonId2);
        List<int> statsList2 = new List<int>(){baseP2.Hp, baseP2.Atk, baseP2.Def, baseP2.Satk, baseP2.Sdef, baseP2.Spd};
        foreach (string stat in battle.P1StatBlock)
        {
            int baseStat = statsList1[battle.P1StatBlock.IndexOf(stat)];
            string[] statPair = stat.Split(":");  
            p1Stats[statPair[0]] = (((baseStat+15)*100+25)/100)+5;
        }
        p1Stats["hp"] += 55;
        foreach (string stat in battle.P2StatBlock)
        {
            int baseStat = statsList2[battle.P2StatBlock.IndexOf(stat)];
            string[] statPair = stat.Split(":");  
            p2Stats[statPair[0]] = (((baseStat+15)*100+25)/100)+5;
        }
        p2Stats["hp"] += 55;
        //reserialize p1Stats, p2Stats to battle statblocks string
        battle.P1StatBlock = new List<string> { $"hp:{p1Stats["hp"]}", $"atk:{p1Stats["atk"]}", $"def:{p1Stats["def"]}", 
        $"satk:{p1Stats["satk"]}", $"sdef:{p1Stats["sdef"]}", $"spd:{p1Stats["spd"]}" };
        battle.P2StatBlock = new List<string> { $"hp:{p2Stats["hp"]}", $"atk:{p2Stats["atk"]}", $"def:{p2Stats["def"]}", 
        $"satk:{p2Stats["satk"]}", $"sdef:{p2Stats["sdef"]}", $"spd:{p2Stats["spd"]}" };
        return battle;
        
    }

    public int CalculatePriority(Battle battle, string pokemon1Move, string pokemon2Move)
    {
        // Get the moves from the database
        var move1 = _context.Move.Where(m => m.Name == pokemon1Move).FirstOrDefault();      
        var move2 = _context.Move.Where(m => m.Name == pokemon2Move).FirstOrDefault();
        //Parse StatBlocks into temporary struct or dictionary/map given that each statblock is a string containing all stats
        // each string should be in the format "hp: 1", "atk: 1", "def: 1", "satk: 1", "sdef: 1", "spd: 1"
        // each stat should be parsed into a string/int pair and stored in a dictionary
        

        // Determine which move goes first taking into account first move priority property, then statblock speed per pokemon
        if (move1.Priority == move2.Priority)
        {
            if (p1Stats["spd"] == p2Stats["spd"])
            {
                return 1;
            }
            else
            {
                return p1Stats["spd"] > p2Stats["spd"] ? 1 : 2;
            }
        }
        else
        {
            return move1.Priority > move2.Priority ? 1 : 2;
        }
    }

    public bool CalculateHit(Battle battle, string move)
    {
        // Get the move from the database where move name = move;
        var move1 = _context.Move.Where(m => m.Name == move).FirstOrDefault();

        // Determine if the move hits
        return move1.Acc > new Random().Next(0, 100);
    }

    public async Task<string> CalculateDamage(Battle battle, string pokemon1Move, string pokemon2Move)
    {
        // Get the moves from the database
        Move move1 = _context.Move.Where(m => m.Name == pokemon1Move).FirstOrDefault();
        Move move2 = _context.Move.Where(m => m.Name == pokemon2Move).FirstOrDefault();

        // Get the attacking and defending pokemon
        Pokemon p1 = _context.Pokemon.Find(battle.PokemonId1);
        Pokemon p2 = _context.Pokemon.Find(battle.PokemonId2);

        battle.P1StatBlock.ForEach(stat => {
            string[] statPair = stat.Split(":");
            p1Stats[statPair[0]] = int.Parse(statPair[1]);
        });
        battle.P2StatBlock.ForEach(stat => {
            string[] statPair = stat.Split(":");
            p2Stats[statPair[0]] = int.Parse(statPair[1]);
        });

        var attackerStats = p1Stats;
        var defenderStats = p2Stats;

        // Get the attacking and defending stats
        float attackerAtk = move1.DamageClass == "Physical" ? attackerStats["atk"] : attackerStats["satk"];
        float defenderDef = move1.DamageClass == "Physical" ? defenderStats["def"] : defenderStats["sdef"];
        float attackerAtk2 = move2.DamageClass == "Physical" ? attackerStats["atk"] : attackerStats["satk"];
        float defenderDef2 = move2.DamageClass == "Physical" ? defenderStats["def"] : defenderStats["sdef"];

        //Look up the attacker's type by querying the database for the pokemon's type from its PokemonId
        //Look up the defender's type by querying the database for the pokemon's type from its PokemonId

        string? p1Type1 = _context.Pokemon.Find(battle.PokemonId1)?.Types[0];
        string? p1Type2 = null;
        if (_context.Pokemon.Find(battle.PokemonId1)?.Types.Count > 1)
        {
            p1Type2 = _context.Pokemon.Find(battle.PokemonId1)?.Types[1];
        }
        string? p2Type1 = _context.Pokemon.Find(battle.PokemonId2)?.Types[0];
        string? p2Type2 = null;
        if (_context.Pokemon.Find(battle.PokemonId2)?.Types.Count > 1)
        {
            p2Type2 = _context.Pokemon.Find(battle.PokemonId2)?.Types[1];
        }

        // Get the STAB multiplier
        float STAB = 1;
        float STAB2 = 1;
        if (p1Type1 == move1.Type || p1Type2 == move1.Type)
        {
            STAB = 1.5f;
        }
        if (p2Type1 == move2.Type || (p2Type2 != null && p2Type2 == move2.Type))
        {
            STAB2 = 1.5f;
        }

        // Get the type multiplier 
        float TMultiplier =  GetTypeMultiplier(move1, p2);
        float TMultiplier2 =  GetTypeMultiplier(move2, p1);

        // Get the random number
        float rand = new Random().Next(217, 255)/255.0f;

        // Calculate the damage
        float? damage = ((22 * move1.Power * attackerAtk/defenderDef / 50f)+2) * STAB * TMultiplier * rand;
        float? damage2 = ((22 * move2.Power * attackerAtk/defenderDef / 50f)+2) * STAB2 * TMultiplier2 * rand;

        // Update the defender's HP
        p1Stats["hp"] -= (int)damage2;
        p2Stats["hp"] -= (int)damage;

        var priority = CalculatePriority(battle, pokemon1Move, pokemon2Move);
        var move1Hit = CalculateHit(battle, pokemon1Move);
        var move2Hit = CalculateHit(battle, pokemon2Move);

        // Return the result
        //string fullString = $"{p1.Name} dealt {damage} damage to {p2.Name}. It was {TMultiplier}x\'s effective. {p2.Name} has {p2Stats["hp"]} HP remaining. {p2.Name} dealt {damage2} damage to {p1.Name}. It was {TMultiplier2}x\'s effective. {p1.Name} has {p1Stats["hp"]} HP remaining.";
        // create a json object to return who has priority, whether the attack has landed, and the remaining hp of each pokemon
        var jsonObject = JsonSerializer.Serialize(new {Priority = priority, Move1Hit = move1Hit, Move2Hit = move2Hit, P1HP = p1Stats["hp"], P2HP = p2Stats["hp"]});
        //string fullString = $"move 1 Power is {move1.Power}, attacker attack is {attackerAtk}, defender defense is {defenderDef}, STAB is {STAB}, TMultiplier is {TMultiplier}, rand is {rand}, damage is {damage}, defender HP is {p2Stats["hp"]}, move 2 Power is {move2.Power}, attacker attack is {attackerAtk2}, defender defense is {defenderDef2}, STAB is {STAB2}, TMultiplier is {TMultiplier2}, rand is {rand}, damage is {damage2}, defender HP is {p1Stats["hp"]}";
        //string fullString = $"attackerAtk over defenderDef is {(attackerAtk/defenderDef)}";
        // string fullString = $"move1 Type is {move1.Type}, move2 Type is {move2.Type}, TMultiplier is {TMultiplier}, TMultiplier2 is {TMultiplier2}, p1 types are {p1Type1} and {p1Type2}, p2 types are {p2Type1} and {p2Type2}";
        //string fullString = $"p2 type 1 is {p2.Types[0]}, move1 type is {move1.Type}, p2 Types count is {p2.Types.Count}";
        //Task<string> result = Task.FromResult(fullString);
        Task<string> result = Task.FromResult(jsonObject);
        return jsonObject;
    }

    public float GetTypeMultiplier(Move move, Pokemon pokemon)
    {
        float m1 =  _context.Types
                .Where(t => t.Name == move.Type)
                .Select(t => pokemon.Types[0] == "bug"?t.Bug:
                             pokemon.Types[0] == "dark"?t.Dark:
                             pokemon.Types[0] == "dragon"?t.Dragon:
                             pokemon.Types[0] == "electric"?t.Electric:
                             pokemon.Types[0] == "fairy"?t.Fairy:
                             pokemon.Types[0] == "fighting"?t.Fighting:
                             pokemon.Types[0] == "fire"?t.Fire:
                             pokemon.Types[0] == "ghost"?t.Ghost:
                             pokemon.Types[0] == "grass"?t.Grass:
                             pokemon.Types[0] == "ice"?t.Ice:
                             pokemon.Types[0] == "normal"?t.Normal:
                             pokemon.Types[0] == "poison"?t.Poison:
                             pokemon.Types[0] == "psychic"?t.Psychic:
                             pokemon.Types[0] == "rock"?t.Rock:  
                             pokemon.Types[0] == "shadow"?t.Shadow:
                             pokemon.Types[0] == "steel"?t.Steel:
                             pokemon.Types[0] == "unknown"?t.Unknown:
                             pokemon.Types[0] == "water"?t.Water:
                             1
                             
                             )

                .FirstOrDefault();
        float m2 = 1;
        if (pokemon.Types.Count == 2)
        {
            m2 =  _context.Types
                .Where(t => t.Name == move.Type)
                .Select(t => pokemon.Types[1] == "bug"?t.Bug:
                             pokemon.Types[1] == "dark"?t.Dark:
                             pokemon.Types[1] == "dragon"?t.Dragon:
                             pokemon.Types[1] == "electric"?t.Electric:
                             pokemon.Types[1] == "fairy"?t.Fairy:
                             pokemon.Types[1] == "fighting"?t.Fighting:
                             pokemon.Types[1] == "fire"?t.Fire:
                             pokemon.Types[1] == "ghost"?t.Ghost:
                             pokemon.Types[1] == "grass"?t.Grass:
                             pokemon.Types[1] == "ice"?t.Ice:
                             pokemon.Types[1] == "normal"?t.Normal:
                             pokemon.Types[1] == "poison"?t.Poison:
                             pokemon.Types[1] == "psychic"?t.Psychic:
                             pokemon.Types[1] == "rock"?t.Rock:  
                             pokemon.Types[1] == "shadow"?t.Shadow:
                             pokemon.Types[1] == "steel"?t.Steel:
                             pokemon.Types[1] == "unknown"?t.Unknown:
                             pokemon.Types[1] == "water"?t.Water:
                             1
                             
                             ).FirstOrDefault();
        }
        return m1 * m2;
        
    }




    //For the moment, maybe we should just persist the battle to table for
    //  duration of combat, and remove it once we resolve the battle and
    //  persist it to the leaderboard. Not great at scale, but to get
    //  something going.

    // They'll pass us battleid, two pokemon, and two move lists. (Post Endpoint)
    // we'll create a battle instance, normalize the pokemon to lvl 50 stats, and persist it. (Result of the Post Endpoint)
    // They'll send us attacking pokemon with their move choice for the round. (Put Endpoint)
    // We'll pass back string builder with attack order/priority, hit/miss, remaining hp and update hp on our end. (Result of the Put Endpoint)
    // They'll let us know if the battle is over so we can remove the battle from the database. (Delete Endpoint)
    
    /*TODO:


    */
}