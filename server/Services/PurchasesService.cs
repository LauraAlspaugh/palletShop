

namespace palletShop.Services;
public class PurchasesService
{
    private readonly PurchasesRepository _purchasesRepository;
    private readonly ListingsService _listingsService;

    public PurchasesService(PurchasesRepository purchasesRepository, ListingsService listingsService)
    {
        _purchasesRepository = purchasesRepository;
        _listingsService = listingsService;
    }

    internal Purchase CreatePurchase(Purchase purchaseData)
    {
        Purchase purchase = _purchasesRepository.CreatePurchase(purchaseData);
        return purchase;
    }
}