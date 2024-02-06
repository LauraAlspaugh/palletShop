

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

    internal List<Receipt> GetReceipts()
    {
        List<Receipt> receipts = _receiptsRepository.GetReceipts();
        return receipts;
    }
}