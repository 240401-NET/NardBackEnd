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

    // public async Task FetchAndStorePokemon()
    // {
    //     var baseUrl = "https://pokeapi.co/api/v2/pokemon/";
    //     for (int i = 1; i <= 151; i++)
    //     {
    //         var response = await _httpClient.GetAsync(baseUrl + i);
    //         response.EnsureSuccessStatusCode();
    //         var jsonData = await response.Content.ReadAsStringAsync();
    //         var pokemon = JsonSerializer.Deserialize<PokemonDto>(jsonData);

    //         var dbPokemon = new Pokemon
    //         {
    //             Id = pokemon.Id,
    //             Name = pokemon.Name,
    //             Types = new List<PokemonTypeContainer> { pokemon.Types[0], pokemon.Types[1] } // Assigning the first type to a list
    //         };

    //         await _context.Pokemon.AddAsync(dbPokemon);
    //     }
    //     await _context.SaveChangesAsync();
    // }

    public async Task<List<Pokemon>> MakePokemonDBTable()
    {
        List<Pokemon> pokemons = await _pokemonRepository.MakePokemonDBTable();
        return(pokemons);
    }
}
