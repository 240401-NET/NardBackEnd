using Models;
using Data;
using Service;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class MoveRepository : IMoveRepository
{
    private readonly ApplicationDbContext _context;    
    private readonly IPokeAPIService _pokeAPIService;

    public MoveRepository(ApplicationDbContext context, IPokeAPIService pokeAPIService)
    {        
        _context  = context;
        _pokeAPIService = pokeAPIService;
    }

    public async Task<List<Move>> MakeMovesTable()
    {
        List<Move> moves = new List<Move>();
        //truncate move table
        await _context.Database.ExecuteSqlRawAsync("Truncate Table Move");
        //make loop
        //https://pokeapi.co/api/v2/move/165
        for(int i = 1; i <= 165; i++)
        {

            Move dbMove = new Move();

            JsonDocument move = await _pokeAPIService.GetMove((i).ToString());

            dbMove.Name = move.RootElement.GetProperty("name").GetString();

            if (move.RootElement.GetProperty("power").ValueKind != JsonValueKind.Null)
            {
                dbMove.Power = move.RootElement.GetProperty("power").GetInt32();
            }
            
            dbMove.DamageClass = move.RootElement.GetProperty("damage_class").GetProperty("name").GetString();

            dbMove.Type = move.RootElement.GetProperty("type").GetProperty("name").GetString();
            
            if (move.RootElement.GetProperty("accuracy").ValueKind != JsonValueKind.Null)
            {
                dbMove.Acc = move.RootElement.GetProperty("accuracy").GetInt32();
            }
            //dbMove.Acc = move.RootElement.GetProperty("accuracy").TryGetInt32(out int accuracy) ? accuracy : -1;

            dbMove.Pp = move.RootElement.GetProperty("pp").TryGetInt32(out int pp) ? pp : 0;

            dbMove.Description = move.RootElement.GetProperty("flavor_text_entries")[0].GetProperty("flavor_text").GetString()?? "No Description";

            dbMove.Priority = move.RootElement.GetProperty("priority").TryGetInt32(out int priority)? priority : 0;

            _context.Move.Add(dbMove);
            moves.Add(dbMove);
        }

        await _context.SaveChangesAsync();
        return moves;
    }
}