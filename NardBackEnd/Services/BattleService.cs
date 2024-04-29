using Models;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;



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

        // build a string with the results of the round
        string roundResult = $"{firstToMove}" +  move1HitString + move2HitString + damageResult;

        return roundResult;
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

    public void NormalizePokemon(Battle battle)
    {
        Pokemon baseP1 = _context.Pokemon.Find(battle.PokemonId1);
        List<int> statsList1 = new List<int>(){baseP1.Hp, baseP1.Atk, baseP1.Def, baseP1.Satk, baseP1.Sdef, baseP1.Spd};
        Pokemon baseP2 = _context.Pokemon.Find(battle.PokemonId2);
        List<int> statsList2 = new List<int>(){baseP2.Hp, baseP2.Atk, baseP2.Def, baseP2.Satk, baseP2.Sdef, baseP2.Spd};
        foreach (string stat in battle.P1StatBlock)
        {
            int baseStat = statsList1[battle.P1StatBlock.IndexOf(stat)];
            string[] statPair = stat.Split(": ");  
            p1Stats[statPair[0]] = (((baseStat+15)*100+25)/100)+5;
        }
        p1Stats["hp"] += 55;
        foreach (string stat in battle.P2StatBlock)
        {
            int baseStat = statsList2[battle.P2StatBlock.IndexOf(stat)];
            string[] statPair = stat.Split(": ");  
            p2Stats[statPair[0]] = (((baseStat+15)*100+25)/100)+5;
        }
        p2Stats["hp"] += 55;
        //reserialize p1Stats, p2Stats to battle statblocks string
        
    }

    public int CalculatePriority(Battle battle, string pokemon1Move, string pokemon2Move)
    {
        // Get the moves from the database
        var move1 = _context.Move.Find(pokemon1Move);
        var move2 = _context.Move.Find(pokemon2Move);

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
        // Get the move from the database
        var move1 = _context.Move.Find(move);

        // Determine if the move hits
        return move1.Acc > new Random().Next(0, 100);
    }

    public async Task<string> CalculateDamage(Battle battle, string pokemon1Move, string pokemon2Move)
    {
        // Get the moves from the database
        var move1 = _context.Move.Find(pokemon1Move);
        var move2 = _context.Move.Find(pokemon2Move);

        // Get the attacking and defending pokemon
        var p1 = _context.Pokemon.Find(battle.PokemonId1);
        var p2 = _context.Pokemon.Find(battle.PokemonId2);

        var attackerStats = p1Stats;
        var defenderStats = p2Stats;

        // Get the attacking and defending stats
        var attackerAtk = move1.DamageClass == "Physical" ? attackerStats["atk"] : attackerStats["sAtk"];
        var defenderDef = move1.DamageClass == "Physical" ? defenderStats["def"] : defenderStats["sDef"];
        var attackerAtk2 = move2.DamageClass == "Physical" ? attackerStats["atk"] : attackerStats["sAtk"];
        var defenderDef2 = move2.DamageClass == "Physical" ? defenderStats["def"] : defenderStats["sDef"];

        //Look up the attacker's type by querying the database for the pokemon's type from its PokemonId
        //Look up the defender's type by querying the database for the pokemon's type from its PokemonId

        var p1Type1 = _context.Pokemon.Find(battle.PokemonId1)?.Types[0];
        var p1Type2 = _context.Pokemon.Find(battle.PokemonId1)?.Types[1];
        var p2Type1 = _context.Pokemon.Find(battle.PokemonId2)?.Types[0];
        var p2Type2 = _context.Pokemon.Find(battle.PokemonId2)?.Types[1];

        // Get the STAB multiplier
        var STAB = (p1Type1 == move1.Type || p1Type2 == move1.Type) ? 1.5 : 1;
        var STAB2 = (p2Type1 == move2.Type || p2Type2 == move2.Type) ? 1.5 : 1;

        // Get the type multiplier 
        var TMultiplier = await GetTypeMultiplier(move1, p2);
        var TMultiplier2 = await GetTypeMultiplier(move2, p1);

        // Get the random number
        var rand = new Random().Next(217, 255);

        // Calculate the damage
        var damage = (22 * move1.Power * (attackerAtk / defenderDef) / 50) * STAB * TMultiplier * rand;
        var damage2 = (22 * move2.Power * (attackerAtk2 / defenderDef2) / 50) * STAB2 * TMultiplier2 * rand;

        // Update the defender's HP
        p1Stats["Hp"] -= (int)damage;
        p2Stats["Hp"] -= (int)damage2;

        // Return the result
        return $"Player 1 dealt {damage} damage to Player 2. Player 2 has {p2Stats["Hp"]} HP remaining. Player 2 dealt {damage2} damage to Player 1. Player 1 has {p1Stats["Hp"]} HP remaining.";
    }

    public async Task<int> GetTypeMultiplier(Move move, Pokemon pokemon)
    {
        int m1 = await _context.Database.ExecuteSqlRawAsync($"SELECT {pokemon.Types[0]} FROM Type WHERE name=@move", new SqlParameter("@move", move));
        int m2 = 1;
        if (pokemon.Types.Count == 2)
        {
            m2 = await _context.Database.ExecuteSqlRawAsync($"SELECT {pokemon.Types[1]} FROM Type WHERE name=@move", new SqlParameter("@move", move));
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

        

      

        Methods:
            InitiateBattle
                -create a Battle instance(receive 2 pkmn, and 2 move lists)
                    -int id
                    -bool(?)/enum(?) state
                    -Pokemon playerPokemon
                        -normalize to lvl 50 stats
                    -Pokemon opponentPokemon
                        -normalize to lvl 50 stats
                    -int playerHP
                    -int opponentHP
                    -List<Move> playerMoves
                    -List<Move> opponentMoves
                -persist Battle
                -return Battle
            ExecuteCombatRound(receive Battle, 2 moves)
                -get order
                    -move1.priority==move2.priority?
                    -move1.priority>move2.priority?
                    -player.speed==opponent.speed?
                    -player.speed>opponent.speed?
                -damage
                    -
                    -loop move list
                        -attackerAtk = 
                        -defenderDef = 
                        -STAB = attacker.Type==move.Type?1.5:1
                        -TMultiplier = getTypeMultiplier(move1,defenderPokemon,0)
                        -rand = rand(217,255)
                        -damage = (((22)*move.power*(attackerAtk/defenderDef))/50)*STAB*TMultiplier*rand
                

            ChangeState
            ResolveDamage

            //public async int getTypeMultiplier(Move move, Pokemon pokemon, int typeNumber)
            // {
                int m1 = await _context.Database.ExecuteSqlRawAsync($"SELECT {pokemon.Types[0]} FROM Type WHERE name={move}");
                int m2 = 1;
                if (pokemon.Types.GetArrayLength == 2){
                    m2 = await _context.Database.ExecuteSqlRawAsync($"SELECT {pokemon.Types[1]} FROM Type WHERE name={move}");
                }
                return (m1*m2);
            //}
    */
}