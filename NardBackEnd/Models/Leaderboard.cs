using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class Leaderboard
{
    [Key]
    public int Id {get; set;}
    [ForeignKey("Pokemon")]
    public int PokemonId {get; set;}
    public int Rank {get; set;}
    public int Win {get; set;}
    public int Loss {get; set;}

}