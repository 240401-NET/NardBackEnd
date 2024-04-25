using Models;

namespace Repository;

public interface IPokemonRepository
{
    Task<List<Pokemon>> MakePokemonDBTable();
}