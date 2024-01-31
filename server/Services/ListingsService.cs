


namespace palletShop.Services;
public class ListingsService
{
    private readonly ListingsRepository _listingsRepository;

    public ListingsService(ListingsRepository listingsRepository)
    {
        _listingsRepository = listingsRepository;
    }

    internal Listing CreateListing(Listing listingData)
    {
        Listing listing = _listingsRepository.CreateListing(listingData);
        return listing;
    }



    internal string DestroyListing(int listingId, string userId)
    {
        Listing listing = GetListingById(listingId, userId);
        if (listing.CreatorId != userId)
        {
            throw new Exception("not your listing to destroy!");

        }
        _listingsRepository.DestroyListing(listingId, userId);
        return "it really is gone";
    }

    internal Listing EditListing(int listingId, Listing listingData, string userId)
    {
        Listing listing = GetListingById(listingId, userId);
        if (listing.CreatorId != userId)
        {
            throw new Exception("not your listing to edit!");
        }

        listing.Name = listingData.Name ?? listing.Name;
        listing.Img = listingData.Img ?? listing.Img;
        listing.Category = listingData.Category ?? listing.Category;
        listing.Description = listingData.Description ?? listing.Description;
        listing.Quantity = listingData.Quantity ?? listing.Quantity;
        _listingsRepository.EditListing(listing);
        return listing;
    }

    internal Listing GetListingById(int listingId, string userId)
    {
        Listing listing = _listingsRepository.GetListingById(listingId);
        if (listing == null || listing.Quantity < 1)
        {
            throw new Exception("not a valid listing id");
        }
        return listing;
    }

    internal List<Listing> GetListings()
    {

        List<Listing> listings = _listingsRepository.GetListings();

        return listings;
    }
}