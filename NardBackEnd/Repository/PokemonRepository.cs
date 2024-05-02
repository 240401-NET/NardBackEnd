using Models;
using Data;
using Service;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

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
        //Truncate Table Pokemon
        await _context.Database.ExecuteSqlRawAsync("Truncate Table Pokemon");
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
            var moves = _context.Move.Select(n=>n.Name).ToList();
            for(int k = 0; k < movesElement.GetArrayLength();k++){ 
                    if (moves.Contains(movesElement[k].GetProperty("move").GetProperty("name").ToString()))
                    {
                        moveList.Add(movesElement[k].GetProperty("move").GetProperty("name").ToString());
                        // Console.WriteLine(moveList[i]);
                    }                   
                }
            // moveList = await assureGen1Move(moves, moveList);
            dbPokemon.MovePool = moveList;
            dbPokemon.Sprite = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{i}.png";
            //post Pokemon to our DB
            _context.Pokemon.Add(dbPokemon);
            pokemons.Add(dbPokemon);
            //savechanges 
            
        }
        //await _context.Database.SqlQuery<Pokemon>($"Truncate Table Pokemon");
        await _context.SaveChangesAsync();
        return pokemons;
    }

    public async Task<List<string>> assureGen1Move(List<Move> moves, List<string> mlist)
    {
        List<string> templist = new List<string>();
        foreach (string s in mlist)
        {
            templist.Add(s);
        }
        ;
        foreach(string s in mlist) 
        {
            // var currMove = await _pokeAPIService.GetMove(s);
            // if (currMove.RootElement.GetProperty("id").GetInt32()>165)
            // {
            //     templist.Remove(s);
            // }
            foreach (Move m in moves)
            {
                if(m.Name==s) 
                {
                   if (m.MoveId>165)
                    {
                        templist.Remove(s);
                     
                    }
                }
            }
        }
        return templist;
    }
}