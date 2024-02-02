




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
        // listing.Quantity--;
        _listingsRepository.EditListing(listing);
        return purchase;
    }

    internal string DestroyPurchase(int purchaseId, string userId)
    {
        Purchase purchase = GetPurchaseById(purchaseId, userId);
        if (purchase.CreatorId != userId)
        {
            throw new Exception("not yours to destroy");
        }

        _purchasesRepository.DestroyPurchase(purchaseId, userId);
        return "it really is gone!";
    }

    internal List<Purchase> GetMyPurchases(string userId)
    {
        List<Purchase> purchases = _purchasesRepository.GetMyPurchases(userId);
        purchases = purchases.FindAll(purchase => purchase.CreatorId == userId);

        return purchases;
    }

    internal Purchase GetPurchaseById(int purchaseId, string id)
    {
        Purchase purchase = _purchasesRepository.GetPurchaseById(purchaseId);
        return purchase;
    }

    internal string ReservePurchase(string id)
    {
        List<Purchase> purchases = GetMyPurchases(id);
        purchases.ForEach(purchase =>
        {
            if (purchase.PurchaseQuantity > purchase.Listing.Quantity)
            {
                throw new Exception("Insufficient Inventory!");
            }
            purchase.Listing.Quantity--;
        });
        purchases.ForEach(purchase =>
        {
            _listingsRepository.EditListing(purchase.Listing);
        });
        return "we have updated these items";
    }

}