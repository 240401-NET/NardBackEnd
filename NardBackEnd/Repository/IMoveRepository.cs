using Models;

namespace Repository;
public interface IMoveRepository
{
    Task<List<Move>> MakeMovesTable();   
}