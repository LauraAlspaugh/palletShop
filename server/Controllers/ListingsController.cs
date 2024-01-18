namespace palletShop.Controllers;
public class ListingsController : ControllerBase
{
    private readonly ListingsService _listingsService;

    public ListingsController(ListingsService listingsService)
    {
        _listingsService = listingsService;
    }
}