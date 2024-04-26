using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Type
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public float Fire { get; set; }
    public float Water { get; set; }
    public float Grass { get; set; }
    public float Electric { get; set; }
    public float Psychic { get; set; }
    public float Dark { get; set; }
    public float Fighting { get; set; }
    public float Flying { get; set; }
    public float Ground { get; set; }
    public float Rock { get; set; }
    public float Steel { get; set; }
    public float Ice { get; set; }
    public float Bug { get; set; }
    public float Poison { get; set; }
    public float Ghost { get; set; }
    public float Dragon { get; set; }
    public float Fairy { get; set; }
    public float Normal { get; set; }
    public float Unknown { get; set; }
    public float Shadow { get; set; }
}