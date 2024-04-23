using System.ComponentModel.DataAnnotations;

namespace Models;

public class Move{
    [Key]
    public int MoveId { get; set; }
    public string Name { get; set; }
    public int Power { get; set; }
    public string Type { get; set; }
    public int Acc { get; set; }
    public int Pp { get; set; }
    public string? Description { get; set; }

};