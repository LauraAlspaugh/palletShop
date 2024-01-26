


namespace palletShop.Services;
public class PurchasesService
{
    private readonly PurchasesRepository _purchasesRepository;
    private readonly ListingsService _listingsService;
    private readonly ListingsRepository _listingsRepository;

    public PurchasesService(PurchasesRepository purchasesRepository, ListingsService listingsService, ListingsRepository listingsRepository)
    {
        _purchasesRepository = purchasesRepository;
        _listingsService = listingsService;
        _listingsRepository = listingsRepository;
    }

    internal Purchase CreatePurchase(Purchase purchaseData, string userId)
    {
        Purchase purchase = _purchasesRepository.CreatePurchase(purchaseData);
        Listing listing = _listingsService.GetListingById(purchaseData.ListingId, userId);
        listing.Quantity--;
        _listingsRepository.EditListing(listing);
        return purchase;
    }

    internal List<Purchase> GetMyPurchases(string userId)
    {
        List<Purchase> purchases = _purchasesRepository.GetMyPurchases(userId);
        purchases = purchases.FindAll(purchase => purchase.CreatorId == userId);
        return purchases;
    }
}