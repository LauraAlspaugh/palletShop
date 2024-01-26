

namespace palletShop.Repositories;
public class PurchasesRepository
{
    private readonly IDbConnection _db;

    public PurchasesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Purchase CreatePurchase(Purchase purchaseData)
    {
        string sql = @"
    INSERT INTO 
    purchases(listingId, creatorId)
    VALUES(@ListingId, @CreatorId);
    SELECT 
    pur.*,
    lis.*
    FROM purchases pur
    JOIN listings lis ON lis.id = pur.listingId

    WHERE pur.id = LAST_INSERT_ID();
";
        Purchase purchase = _db.Query<Purchase, Listing, Purchase>(sql, (purchase, listing) =>
              {
                  //   purchase.Creator = account;
                  purchase.Listing = listing;
                  return purchase;
              }, purchaseData).FirstOrDefault();
        return purchase;
    }

    internal List<Purchase> GetMyPurchases(string userId)
    {
        string sql = @"
       SELECT 
       pur.*,
       lis.*
       FROM purchases pur
       JOIN listings lis ON lis.id = pur.listingId
       WHERE pur.creatorId = @UserId;
       ";

        List<Purchase> purchases = _db.Query<Purchase, Listing, Purchase>(sql, (purchase, listing) =>
        {
            // purchase.Creator = account;
            purchase.Listing = listing;
            return purchase;
        }, new { userId }).ToList();
        return purchases;
    }
}