using Service;
using Models;
using System.Text.Json; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokeAPIService _pokeAPIService;
    private readonly IPokemonService _pokeService;

    public PokemonController(IPokeAPIService pokeAPIService, IPokemonService pokemonService)
    {
        _pokeAPIService = pokeAPIService;
        _pokeService = pokemonService;
    }


    [HttpPost("makepokemontable")]
    public async Task<List<Pokemon>> MakePokemonDBTable()
    {
        List<Pokemon> pokemonsDb = await _pokeService.MakePokemonDBTable();
        return pokemonsDb;
    }

    [HttpGet("searchPokemon/{pokemon}")]
    public async Task<IActionResult> SearchPokemon(string pokemon)
    {
        try
        {
            var poke = await _pokeService.SearchPokemon(pokemon);
            return Ok(poke);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error accessing ContextDb for Pokémon");
        }
    }

    //Make a method to get pokemon by id.
    [HttpGet("getPokemon/{id}")]
    public async Task<IActionResult> GetPokemon(int id)
    {
        try
        {
            var poke = await _pokeService.GetPokemon(id);
            return Ok(poke);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error accessing ContextDb for Pokémon");
        }
    }

    [HttpGet("getAllPokemon")]
    public async Task<IActionResult> GetAllPokemon()
    {
        try
        {
            var pokemons = await _pokeService.GetAllPokemon();
            return Ok(pokemons);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error accessing Db for Pokémon");
        }
    }

    [HttpGet ("getRandomPokemon")]
    public async Task<IActionResult> GetRandomPokemon()
    {
        
        var randoMon = await _pokeService.GetPokemon(new Random().Next(1,151));;
        return Ok(randoMon);
    }
}