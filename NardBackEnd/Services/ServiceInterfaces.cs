using Models;
using System.Text.Json;

namespace Service;

public interface IBattleService
{

}

public interface IPokemonService
{
    //Task FetchAndStorePokemon();
    Task<List<Pokemon>> MakePokemonDBTable();
}

public interface IAudioService
{

}

public interface IImageService
{

}

public interface IPokeAPIService
{
    Task<IEnumerable<Pokemon>> GetGen1PokemonAsync();
    Task<JsonDocument> GetPokemon(string pokemon);
}