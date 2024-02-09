


namespace palletShop.Services;
public class ReceiptsService
{
    private readonly ReceiptsRepository _receiptsRepository;

    public ReceiptsService(ReceiptsRepository receiptsRepository)
    {
        _receiptsRepository = receiptsRepository;
    }

    internal Receipt CreateReceipt(Receipt receiptData, string id)
    {
        Receipt receipt = _receiptsRepository.CreateReceipt(receiptData, id);
        return receipt;
    }
    internal Receipt GetReceiptById(int receiptId)
    {
        Receipt receipt = _receiptsRepository.GetReceiptById(receiptId);
        if (receipt == null)
        {
            throw new Exception("not a valid receipt id");
        }
        return receipt;
    }

    internal string DestroyReceipt(int receiptId, string userId)
    {
        Receipt receipt = GetReceiptById(receiptId);
        if (receipt.CreatorId != userId)
        {
            throw new Exception("don't try it bro");
        }
        _receiptsRepository.DestroyReceipt(receiptId, userId);
        return "It is gone";
    }

    internal List<Receipt> GetReceipts()
    {
        List<Receipt> receipts = _receiptsRepository.GetReceipts();
        return receipts;
    }
}