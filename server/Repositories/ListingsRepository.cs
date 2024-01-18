namespace palletShop.Repositories;
public class ListingsRepository
{
    private readonly IDbConnection _db;

    public ListingsRepository(IDbConnection db)
    {
        _db = db;
    }
}