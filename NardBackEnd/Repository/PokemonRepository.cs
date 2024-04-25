using Models;
using Data;
using Data;
using Service;
using System.Text.Json;

namespace Repository;

public class PokemonRepository : IPokemonRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IPokeAPIService _pokeAPIService;


    public PokemonRepository(ApplicationDbContext context, IPokeAPIService apiService){
        _pokeAPIService = apiService;
        _context  = context;
    }


    public async Task<List<Pokemon>> MakePokemonDBTable()//returning async void might be problem
    {
        
        List<Pokemon> pokemons = new List<Pokemon>();
        
        //make a loop
        for (int i = 1;i<=151;i++)
            {
            Pokemon dbPokemon = new Pokemon();    
            //get a pokemon from pokeAPI
            JsonDocument pokemon = await _pokeAPIService.GetPokemon((i).ToString());
            //make pokemon from pokemon
          
            //Name
            dbPokemon.Name = pokemon.RootElement.GetProperty("name").GetString();
            //List<Type> Types
            List<string> typeList = new List<string>();
            var typeElement = pokemon.RootElement.GetProperty("types");
            for(int j = 0; j<typeElement.GetArrayLength();j++)
            {
                typeList.Add(typeElement[j].GetProperty("type").GetProperty("name").ToString());
            }
            dbPokemon.Types = typeList;
            //Hp
            dbPokemon.Hp = pokemon.RootElement.GetProperty("stats")[0].GetProperty("base_stat").GetInt32();            
            //Atk
            dbPokemon.Atk = pokemon.RootElement.GetProperty("stats")[1].GetProperty("base_stat").GetInt32();
            //Satk
            dbPokemon.Satk = pokemon.RootElement.GetProperty("stats")[2].GetProperty("base_stat").GetInt32();
            //Def
            dbPokemon.Def = pokemon.RootElement.GetProperty("stats")[3].GetProperty("base_stat").GetInt32();
            //Sdef
            dbPokemon.Sdef = pokemon.RootElement.GetProperty("stats")[4].GetProperty("base_stat").GetInt32();
            //Spd
            dbPokemon.Spd = pokemon.RootElement.GetProperty("stats")[5].GetProperty("base_stat").GetInt32();
            //List<move>
            List<string> moveList = new List<string>();
            var movesElement = pokemon.RootElement.GetProperty("moves");
            for(int k = 0; k < movesElement.GetArrayLength();k++){                    
                    moveList.Add(movesElement[k].GetProperty("move").GetProperty("name").ToString());
                    // Console.WriteLine(moveList[i]);
                }
            dbPokemon.MovePool = moveList;
            //post Pokemon to our DB
            _context.Pokemon.Add(dbPokemon);
            pokemons.Add(dbPokemon);
            //savechanges 
            
            }
            await _context.SaveChangesAsync();
            return pokemons;
    }
}