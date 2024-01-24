
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
    acc.*
    FROM purchases pur
    JOIN accounts acc ON pur.creatorId = acc.id
    WHERE pur.id = LAST_INSERT_ID();
";
        Purchase purchase = _db.Query<Purchase, Account, Purchase>(sql, (purchase, account) =>
              {
                  purchase.Creator = account;
                  return purchase;
              }, purchaseData).FirstOrDefault();
        return purchase;
    }
}