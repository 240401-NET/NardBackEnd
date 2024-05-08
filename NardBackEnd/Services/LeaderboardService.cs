using Data;
using Models;

namespace Service;

public class LeaderboardService:ILeaderboardService
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;
    private readonly IPokemonService _pokemonService;
    public LeaderboardService(HttpClient httpClient, ApplicationDbContext context, IPokemonService pokemonService){
        _context = context;
        _httpClient = httpClient;  
        _pokemonService = pokemonService;      
    }

    public void CreateLeaderboard(Leaderboard leaderboard)
    {
        //check to see if the pokemon id already exists in a leaderboard and then if it doesn't add a leaderboard entry
        var leaderboards = _context.Leaderboards.ToList();
        foreach (var lb in leaderboards)
        {
            if (lb.PokemonId == leaderboard.PokemonId)
            {
                return;
            }
        }
        _context.Leaderboards.Add(leaderboard);
        _context.SaveChanges();
    }

    // In your LeaderboardService
    public async Task CreateInitialLeaderboard()
    {
        var allPokemon = await _pokemonService.GetAllPokemon();
        foreach (var pokemon in allPokemon)
        {
            var leaderboard = new Leaderboard
            {
                PokemonId = pokemon.Id,
                PokemonName = pokemon.Name,
                Rank = 0,
                Win = 0,
                Loss = 0
            };
            _context.Leaderboards.Add(leaderboard);
        }
        await _context.SaveChangesAsync();
    }

    public void UpdateLeaderboard(Leaderboard leaderboard, int winLossPoint)
    {
        //check if leaderboard is in the database
        if (_context.Leaderboards.Find(leaderboard.Id) == null)
        {
            //if it is not in the database, add it
            _context.Leaderboards.Add(leaderboard);
        }
        if (winLossPoint > 0)
        {
            leaderboard.Win += winLossPoint;
        }
        else if (winLossPoint < 0)
        {
            leaderboard.Loss -= winLossPoint;
        }
        // get all leaderboards ordered by win/loss ratio
        var leaderboards = _context.Leaderboards.OrderByDescending(lb => lb.Loss != 0 ? lb.Win / lb.Loss : lb.Win).ToList();
        // set the rank of each leaderboard
        for (int i = 0; i < leaderboards.Count; i++)
        {
            Console.WriteLine(leaderboards[i].Rank);
            leaderboards[i].Rank = i + 1;
            Console.WriteLine(leaderboards[i].Rank);
            _context.Leaderboards.Update(leaderboards[i]);
        }
        _context.SaveChanges();
    }

    public void DeleteLeaderboard(int leaderboardId)
    {
        var leaderboard = _context.Leaderboards.Find(leaderboardId);
        _context.Leaderboards.Remove(leaderboard);
        _context.SaveChanges();
    }

    public Leaderboard GetLeaderboard(int leaderboardId)
    {
        return _context.Leaderboards.Find(leaderboardId);
    }

    public List<Leaderboard> GetLeaderboards()
    {
        return _context.Leaderboards.OrderBy(lb => lb.Rank).ToList();
    }

}