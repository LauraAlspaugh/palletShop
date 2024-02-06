import { AppState } from "../AppState.js"
import { Receipt } from "../models/Receipt.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ReceiptsService{
async createReceipt(receiptData){
    const res = await api.post('api/receipts', receiptData)
    logger.log("creating a receipt!")
    const newReceipt = new Receipt(res.data)
    AppState.receipts.push(newReceipt)
    return newReceipt
}
}
export const receiptsService = new ReceiptsService()