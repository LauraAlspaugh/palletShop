namespace palletShop.Services;
public class ListingsService
{
    private readonly ListingsRepository _listingsRepository;

    public ListingsService(ListingsRepository listingsRepository)
    {
        _listingsRepository = listingsRepository;
    }
}