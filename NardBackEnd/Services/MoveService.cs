using Models;
using Data;
using Repository;
using System.Text.Json;
using System.Text;

namespace Service;

public class MoveService : IMoveService
{
    private readonly HttpClient _client;
    private readonly IMoveRepository _repo;
    private readonly ApplicationDbContext _context;
    private readonly IPokemonService _pokeservice;

    public MoveService(HttpClient httpClient, IMoveRepository moveRepository, ApplicationDbContext context, IPokemonService pokemonService)
    {
        _client = httpClient;
        _repo = moveRepository;
        _context = context;
        _pokeservice = pokemonService;
    }
    public async Task<List<Move>> MakeMovesTable()
    {
        List<Move> moves = await _repo.MakeMovesTable();
        return(moves);
    }
    public async Task<List<Move>> GetMoves()
    {
        return _context.Move.ToList();
    }

    public async Task<Move> GetMove(int moveId)
    {
        return _context.Move.Find(moveId);
    }
    public async Task<Move> GetMoveByName(string name){
        return _context.Move.Where(p => p.Name == name).FirstOrDefault();
    }

    public string GetRandomMoveSet(Pokemon p)
    {
        
        var rand = new Random();
        string moves = "";
        int maxindex = 4;

        if (p.MovePool.Count<4)
        {
            maxindex = p.MovePool.Count;
        }

        HashSet<int> selectedIndexes = new HashSet<int>();
        while (selectedIndexes.Count < maxindex)
        {
            int index = rand.Next(p.MovePool.Count);
            selectedIndexes.Add(index);

        }
        StringBuilder builder = new StringBuilder();
        foreach (int index in selectedIndexes)
        {
            builder.Append(p.MovePool[index]);
            builder.Append(",");
        }
        builder.Length-=1;
        moves = builder.ToString();
                
        return  moves;
    }
}