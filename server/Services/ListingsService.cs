


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

    internal Listing EditListing(int listingId, Listing listingData, string userId)
    {
        Listing listing = GetListingById(listingId);
        if (listing.CreatorId != userId)
        {
            throw new Exception("not your listing to edit!");
        }

        listing.Name = listingData.Name ?? listing.Name;
        listing.Img = listingData.Img ?? listing.Img;
        listing.Category = listingData.Category ?? listing.Category;
        listing.Description = listingData.Description ?? listing.Description;
        _listingsRepository.EditListing(listing);
        return listing;
    }

    internal Listing GetListingById(int listingId)
    {
        Listing listing = _listingsRepository.GetListingById(listingId);
        if (listing == null)
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