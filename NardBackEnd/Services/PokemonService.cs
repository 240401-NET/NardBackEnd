using Models;
using Data;
using Repository;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Service;



public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;
    private IPokemonRepository _pokemonRepository;
    private readonly ApplicationDbContext _context;

    public PokemonService(HttpClient httpClient, ApplicationDbContext context, IPokemonRepository repo)
    {
        _httpClient = httpClient;
        _context = context;
        _pokemonRepository = repo;
    }

    public async Task<List<Pokemon>> MakePokemonDBTable()
    {
        List<Pokemon> pokemons = await _pokemonRepository.MakePokemonDBTable();
        return(pokemons);
    }

    public async Task<Pokemon> SearchPokemon(string pokemonName)
    {
        // check the context database for the pokemon by name
        var pokemon = await _context.Pokemon.FirstOrDefaultAsync(p => p.Name == pokemonName);
        if (pokemon != null)
        {
            return pokemon;
        }
        else
        {
            return null;
        }
    }

    public async Task<Pokemon> GetPokemon(int pokemonId)
    {
        // check the context database for the pokemon by id
        var pokemon = await _context.Pokemon.FirstOrDefaultAsync(p => p.Id == pokemonId);
        if (pokemon != null)
        {
            return pokemon;
        }
        else
        {
            return null;
        }
        
    }

    public async Task<IEnumerable<Pokemon>> GetAllPokemon()
    {
        // get all pokemon from the context database
        var pokemons = await _context.Pokemon.ToListAsync();
        if (pokemons != null)
        {
            return pokemons;
        }
        else
        {
            return null;
        }
    }
}
