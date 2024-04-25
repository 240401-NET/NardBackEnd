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

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Pokemon>> GetPokemon(int id)
    // {
    //     try
    //     {
    //         var pokemon = await _pokeAPIService.GetPokemonAsync(id);
    //         if (pokemon == null)
    //             return NotFound("Pokémon not found.");
    //         return Ok(pokemon);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, "Internal server error.");
    //     }
    // }

    // [HttpGet("allgen1")]
    // public async Task<IActionResult> GetAllGen1Pokemon()
    // {
    //     try
    //     {
    //         var pokemons = await _pokeAPIService.GetGen1PokemonAsync();
    //         return Ok(pokemons);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, "Error accessing PokéAPI for Gen 1 Pokémon");
    //     }
    // }

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

    [HttpGet("getPokemon")]
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
}