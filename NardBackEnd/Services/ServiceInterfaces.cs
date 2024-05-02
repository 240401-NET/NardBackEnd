using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;
using System.Collections.Generic;
using System.Text.Json;

namespace Service;

public interface IBattleService
{
    void CreateBattle(Battle battle);
    void UpdateBattle(Battle battle);
    string UpdateBattle(Battle battle, int firstToMove, bool move1Hit, bool move2Hit, Task<string> damageResult);
    void DeleteBattle(int battleId);
    int CalculatePriority(Battle battle, string pokemon1Move, string pokemon2Move);
    bool CalculateHit(Battle battle, string move);
    Task<string> CalculateDamage(Battle battle, string pokemon1Move, string pokemon2Move);
    Battle NormalizePokemon(Battle battle);
    Battle GetBattle(int battleId);
    List<Battle> GetBattles();

}

public interface IPokemonService
{
    Task<List<Pokemon>> MakePokemonDBTable();
    Task<Pokemon> SearchPokemon(string pokemonName);
    Task<Pokemon> GetPokemon(int pokemonId);
    Task<IEnumerable<Pokemon>> GetAllPokemon();

}

public interface IPokeAPIService
{
    // Task<IEnumerable<Pokemon>> GetGen1PokemonAsync();
    Task<JsonDocument> GetPokemon(string pokemon);
    Task<JsonDocument> GetMove(string moveId);
    Task<JsonDocument> GetPokeType(int type);
}

public interface IMoveService
{
    // Task<List<Move>> GetMoves();
    // Task<Move> GetMove(string moveId);
    Task<List<Move>> MakeMovesTable();
    Task<List<Move>> GetMoves();
    Task<Move> GetMove(int moveId);
    Task<Move> GetMoveByName(string name);
    string[] GetRandomMoveSet(Pokemon pokemon);
}

public interface ITypeService
{
    // Task<List<Type>> GetTypes();
    // Task<Type> GetType(string typeId);
    Task<List<Models.Type>> MakeTypeDBTable();
}

public interface ILeaderboardService
{
    void CreateLeaderboard(Leaderboard leaderboard);
    void UpdateLeaderboard(Leaderboard leaderboard, int winLossPoint);
    void DeleteLeaderboard(int leaderboardId);
    Leaderboard GetLeaderboard(int leaderboardId);
    List<Leaderboard> GetLeaderboards();
    
}