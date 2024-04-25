using Models;
using Data;
using Repository;
using System.Text.Json;

namespace Service;

public class MoveService : IMoveService
{
    private readonly HttpClient _client;
    private readonly IMoveRepository _repo;
    private readonly ApplicationDbContext _context;
    // public async Task<List<MoveService>> GetMoves()
    // {
    //     throw new System.NotImplementedException();
    // }

    // public async Task<Move> GetMove(string moveId)
    // {
    //     throw new System.NotImplementedException();
    // }
    public MoveService(HttpClient httpClient, IMoveRepository moveRepository, ApplicationDbContext context)
    {
        _client = httpClient;
        _repo = moveRepository;
        _context = context;
    }
    public async Task<List<Move>> MakeMovesTable()
    {
        List<Move> moves = await _repo.MakeMovesTable();
        return(moves);
    }
}