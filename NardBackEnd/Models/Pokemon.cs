using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;
public class Pokemon
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Types { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    public int Satk { get; set; }
    public int Def { get; set; }
    public int Sdef { get; set; }
    public int Spd { get; set; }
    public List<string> MovePool { get; set; }

    // [ForeignKey("Move")]
    // public int MoveId1 { get; set; }
    // [ForeignKey("Move")]
    // public int MoveId2 { get; set; }
    // [ForeignKey("Move")]
    // public int MoveId3 { get; set; }
    // [ForeignKey("Move")]
    // public int MoveId4 { get; set; }
}//we may need to make a second model with the moves 1-4 for the actual battle