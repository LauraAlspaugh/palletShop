
namespace palletShop.Repositories;
public class ReceiptsRepository
{
    private readonly IDbConnection _db;

    public ReceiptsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Receipt CreateReceipt(Receipt receiptData, string id)
    {
        string sql = @"
    INSERT INTO 
    receipts(buyer, street, city, state1, zip, total, purchaseId, creatorId)
    VALUES(@Buyer, @Street, @City, @State1, @Zip, @Total, @PurchaseId, @CreatorId);
    SELECT 
    rec.*,
    acc.*
    FROM receipts rec
    JOIN accounts acc ON rec.creatorId = acc.id
WHERE rec.id = LAST_INSERT_ID();
";
        Receipt receipt = _db.Query<Receipt, Account, Receipt>(sql, (receipt, account) =>
              {
                  //   purchase.Creator = account;
                  receipt.Creator = account;
                  return receipt;
              }, receiptData).FirstOrDefault();
        return receipt;
    }
}