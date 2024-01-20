
using Microsoft.AspNetCore.Http.HttpResults;

namespace palletShop.Repositories;
public class ListingsRepository
{
    private readonly IDbConnection _db;

    public ListingsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Listing CreateListing(Listing listingData)
    {
        string sql = @"
    INSERT INTO 
    listings(name, description, price, img, quantity, category, creatorId)
    VALUES(@Name, @Description, @Price, @Img, @Quantity, @Category, @CreatorId);
    SELECT 
    lis.*,
    acc.*
    FROM listings lis
    JOIN accounts acc ON lis.creatorId = acc.id
    WHERE lis.id = LAST_INSERT_ID();
";
        Listing listing = _db.Query<Listing, Account, Listing>(sql, (listing, account) =>
             {
                 listing.Creator = account;
                 return listing;
             }, listingData).FirstOrDefault();
        return listing;
    }

    internal Listing EditListing(Listing listing)
    {
        string sql = @"
    UPDATE listings 
    SET 
    
name = @Name,
description = @Description,
img = @img,
category = @Category,
quantity = @Quantity
WHERE id = @Id;

SELECT lis.*,
    acc.*
    FROM listings lis
    JOIN accounts acc ON lis.creatorId = acc.id
    Where lis.id = @Id;
    ";
        Listing newListing = _db.Query<Listing, Account, Listing>(sql, (listing, account) =>
      {
          listing.Creator = account;
          return listing;
      }, listing).FirstOrDefault();
        return newListing;
    }

    internal Listing GetListingById(int listingId)
    {

        string sql = @"
       SELECT 
       lis.*,
       acc.*
       FROM listings lis
       JOIN accounts acc ON lis.creatorId = acc.id
       WHERE lis.id = @listingId;
       ";
        Listing listing = _db.Query<Listing, Account, Listing>(sql, (listing, account) =>
        {
            listing.Creator = account;
            return listing;
        }, new { listingId }).FirstOrDefault();
        return listing;
    }

    internal List<Listing> GetListings()
    {
        string sql = @"
    SELECT 
    lis.*,
    acc.*
    FROM listings lis
    JOIN accounts acc ON lis.creatorId = acc.id
    ";
        List<Listing> listings = _db.Query<Listing, Account, Listing>(sql, (listing, account) =>
        {
            listing.Creator = account;
            return listing;
        }).ToList();
        return listings;
    }
}