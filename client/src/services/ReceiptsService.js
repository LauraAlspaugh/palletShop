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
async getReceipts(){
    const res = await api.get('api/receipts')
    logger.log('getting receipts', res.data)
    AppState.receipts = res.data.map(pojo => new Receipt(pojo))
}
async destroyReceipt(receiptId){
    const res = await api.delete(`api/receipts/${receiptId}`)
    logger.log('deleting this receipt!', res.data)
    AppState.receipts = AppState.receipts.filter((receipt) => receipt.id != receiptId)
}
}
export const receiptsService = new ReceiptsService()