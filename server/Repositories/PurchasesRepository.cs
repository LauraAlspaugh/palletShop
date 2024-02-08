




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

    internal void DestroyPurchase(int purchaseId, string userId)
    {
        string sql = @"
DELETE FROM purchases WHERE id = @purchaseId LIMIT 1;
SELECT pur.*,
    acc.*
    FROM purchases pur
    JOIN accounts acc ON pur.creatorId = acc.id
    Where pur.id = @purchaseId;
";
        _db.Execute(sql, new { purchaseId });
    }

    internal void EmptyCart(string creatorId)
    {
        string sql = @"
       DELETE FROM purchases WHERE creatorId = @creatorId;
       ";
        _db.Execute(sql, new { creatorId });
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

    internal Purchase GetPurchaseById(int purchaseId)
    {
        string sql = @"
       SELECT 
       pur.*,
       acc.*
       FROM purchases pur
       JOIN accounts acc ON pur.creatorId = acc.id
       WHERE pur.id = @purchaseId;
       ";
        Purchase purchase = _db.Query<Purchase, Account, Purchase>(sql, (purchase, account) =>
         {
             purchase.Creator = account;
             return purchase;
         }, new { purchaseId }).FirstOrDefault();
        return purchase;
    }

    internal Purchase ReservePurchase(Purchase purchaseData)
    {
        throw new NotImplementedException();
    }
}