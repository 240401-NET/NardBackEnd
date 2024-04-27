using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Battle
{
    [Key]
    public int BattleId { get; set; }
    public Status BattleStatus { get; set; }
    public Winner BattleWinner { get; set; }
    [ForeignKey("Pokemon")]
    public int PokemonId1 { get; set; }
    public List<string> P1StatBlock { get; set; }
    public List<string> P1Moves { get; set; }
    [ForeignKey("Pokemon")]
    public int PokemonId2 { get; set; }
    public List<string> P2StatBlock { get; set; }
    public List<string> P2Moves { get; set; }
    public BattlePhase battlePhase { get; set; }


    public enum BattlePhase
    {
        Selection,
        Attack,
        Resolution,
        End
    }

    public enum Status
    {
        InProgress,
        Finished
    }
    public enum Winner
    {
        P1,
        P2,
        Draw,
        NotFinished
    }
}