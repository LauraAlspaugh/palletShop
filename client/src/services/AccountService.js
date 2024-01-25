import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Purchase } from '../models/Purchase.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async getMyPurchases(){
    const res = await api.get('/account/purchases')
    logger.log('getting my purchases!', res.data)
    AppState.purchases = res.data.map((purchase)=> new Purchase(purchase))
}
}

export const accountService = new AccountService()
