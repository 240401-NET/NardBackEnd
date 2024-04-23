using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Leaderboard
{
    public int Id {get; set;}
    [ForeignKey("Pokemon")]
    public int PokemonId {get; set;}
    public string Rank {get; set;}
    public string WinLoss {get; set;}

}