namespace palletShop.Models;
public class Purchase
{
    public int Id { get; set; }
    public int ListingId { get; set; }
    public string CreatorId { get; set; }
    public Account Creator { get; set; }

}