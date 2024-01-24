import { AppState } from "../AppState.js"
import { Listing } from "../models/Listing.js"
import { Purchase } from "../models/Purchase.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ListingsService{
async getListings(){
    const res = await api.get('api/listings')
    logger.log('getting listings', res.data)
    AppState.listings = res.data.map(pojo => new Listing(pojo))
}
async getListingById(listingId){
    AppState.activeListing = null
    const res = await api.get(`api/listings/${listingId}`)
   logger.log('getting listing by id', res.data)
   AppState.activeListing = new Listing(res.data)
   }
   async createListing(listingData){
    const res = await api.post('api/listings', listingData)
    logger.log('creating listing!', res.data)
    const newListing = new Listing(res.data)
    AppState.listings.push(newListing)
    return newListing
}
async createPurchase(listingId){
const res = await api.post('api/purchases', {listingId})
logger.log('You have purchased this!' , res.data)
AppState.purchases.push(new Purchase(res.data))
}
}
export const listingsService = new ListingsService()