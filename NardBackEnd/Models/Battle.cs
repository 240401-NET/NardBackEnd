using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Battle{
    public int BattleId { get; set; }
    public string Status { get; set; }
    public string Winner { get; set; }
     [ForeignKey("Pokemon")]
    public int Pokemon1 { get; set; }//since pokemon are foreign keys
     [ForeignKey("Pokemon")]
    public int Pokemon2 { get; set; }// should they be type pokemon?
}