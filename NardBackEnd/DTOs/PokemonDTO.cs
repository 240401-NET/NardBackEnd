namespace DTOs;

class PokemonDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PokemonTypeContainer> Types { get; set; }
}

public class PokemonTypeContainer
{
    public PokemonType Type { get; set; }
}

public class PokemonType
{
    public string Name { get; set; }
}