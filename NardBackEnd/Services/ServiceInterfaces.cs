using Models;
using System.Collections.Generic;
using System.Text.Json;

namespace Service;

public interface IBattleService
{

}

public interface IPokemonService
{
    Task<List<Pokemon>> MakePokemonDBTable();
    Task<Pokemon> SearchPokemon(string pokemonName);
    Task<Pokemon> GetPokemon(int pokemonId);
    Task<IEnumerable<Pokemon>> GetAllPokemon();
}

public interface IAudioService
{

}

public interface IImageService
{

}

public interface IPokeAPIService
{
    // Task<IEnumerable<Pokemon>> GetGen1PokemonAsync();
    Task<JsonDocument> GetPokemon(string pokemon);
    Task<JsonDocument> GetMove(string moveId);
}

public interface IMoveService
{
    // Task<List<Move>> GetMoves();
    // Task<Move> GetMove(string moveId);
    Task<List<Move>> MakeMovesTable();
}