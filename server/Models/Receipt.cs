namespace palletShop.Models;
public class Receipt
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Buyer { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State1 { get; set; }
    public string Zip { get; set; }
    public int Total { get; set; }
    public Account Creator { get; set; }
    public string CreatorId { get; set; }
    public int PurchaseId { get; set; }
}