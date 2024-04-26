namespace Repository;

public interface ITypeRepository
{
    Task<List<Models.Type>> MakeTypeDBTable();
}