using Service;
using Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Data;
using Microsoft.EntityFrameworkCore;


namespace Controllers;
[ApiController]
[Route("[Controller]")]

public class LeaderboardController : ControllerBase
{ 

    private readonly ApplicationDbContext _context;
    private readonly HttpClient _client;
    private readonly IPokemonService _pokemonService;
    private readonly IMoveService _moveService;
    private readonly ITypeService _typeService;
    private readonly IPokeAPIService _pokeAPIService;
    private readonly IBattleService _battleService;
    private readonly ILeaderboardService _leaderboardService;

    public LeaderboardController(ApplicationDbContext context, HttpClient client, IPokemonService pokemonService, IMoveService moveService, ITypeService typeService, IPokeAPIService pokeAPIService, IBattleService battleService, ILeaderboardService leaderboardService)
    {
        _context = context;
        _client = client;
        _pokemonService = pokemonService;
        _moveService = moveService;
        _typeService = typeService;
        _pokeAPIService = pokeAPIService;
        _battleService = battleService;
        _leaderboardService = leaderboardService;
    }

    // Post a leaderboard
    [HttpPost ("createLeaderboard")]
    public async Task<ActionResult<Leaderboard>> PostLeaderboard(Leaderboard leaderboard)
    {
       _leaderboardService.CreateLeaderboard(leaderboard);

        return Ok(leaderboard);
    }

    // In your LeaderboardController
    [HttpPost("createInitialLeaderboard")]
    public async Task<ActionResult> CreateInitialLeaderboard()
    {
        await _leaderboardService.CreateInitialLeaderboard();
        return Ok();
    }

    // Delete a leaderboard
    [HttpDelete("{id}")]
    public async Task<ActionResult<Leaderboard>> DeleteLeaderboard(int id)
    {
        var leaderboard = _leaderboardService.GetLeaderboard(id);
        if (leaderboard == null)
        {
            return NotFound();
        }
        else{
            _leaderboardService.DeleteLeaderboard(leaderboard.Id);
            return Ok(leaderboard);
        }
    }

    // Get a leaderboard entry
    [HttpGet("{id}/entry")]
    public async Task<ActionResult<Leaderboard>> GetLeaderboardEntry(int id)
    {
        var leaderboard = _leaderboardService.GetLeaderboard(id);
        if (leaderboard == null)
        {
            return NotFound();
        }
        else{
            return Ok(leaderboard);
        }
    }
    // update a leaderboard entry
    [HttpPut("{id}/entry")]
    public async Task<IActionResult> PutLeaderboardEntry(int id, int winLossPoint)
    {
        // use the get leaderboard service method to get the leaderboard
        var leaderboard = _leaderboardService.GetLeaderboard(id);
        if (leaderboard == null)
        {
            return NotFound();
        }
        else{
            // call a service method to add the winloss point and update the rank if necessary.
            _leaderboardService.UpdateLeaderboard(leaderboard, winLossPoint);
            return Ok();
        }
    }

    // Get all leaderboards
    [HttpGet ("getLeaderboards")]
    public async Task<ActionResult<IEnumerable<Leaderboard>>> GetLeaderboards()
    {
        var leaderboards = _leaderboardService.GetLeaderboards();
        if (leaderboards == null)
        {
            return NotFound();
        }
        else{
            return Ok(leaderboards);
        }
    }
 
}