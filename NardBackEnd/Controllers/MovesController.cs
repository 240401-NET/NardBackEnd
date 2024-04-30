using Service;
using Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

[ApiController]
[Route("[Controller]")]
public class MovesController : ControllerBase
{
    private readonly IMoveService _moveService;

    public MovesController(IMoveService moveService)
    {
        _moveService = moveService;
    }

    [HttpPost("makeMovesTable")]
    public async Task<List<Move>> MakeMovesTable()
    {
        List<Move> movesDb = await _moveService.MakeMovesTable();
        return movesDb;
    }

    [HttpGet("allMoves")]
    public async Task<IActionResult> GetAllMoves()
    {
        try
        {
            var moves = await _moveService.GetMoves();
            return Ok(moves);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error accessing Database for moves");
        }
    }

    [HttpGet("getMove/{moveId}")]
    public async Task<IActionResult> GetMove(int moveId)
    {
        try
        {
            var move = await _moveService.GetMove(moveId);
            return Ok(move);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error accessing Database for moves");
        }
    }

    // add a get moves by name
    [HttpGet("getMoveByName/{name}")]
    public async Task<IActionResult> GetMoveByName(string name)
    {
        try
        {
            var move = await _moveService.GetMoveByName(name);
            return Ok(move);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Error accessing Database for moves");
        }
    }
}