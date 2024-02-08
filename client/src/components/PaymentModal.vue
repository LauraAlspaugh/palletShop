<template>
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Stripe Payment(insert third party)</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-center">
                    Enter Address Info.
                    <form @submit.prevent="createReceipt()">
                        <div class="mb-3 p-3">
                            <label for="buyer" class="form-label">Name</label>
                            <input v-model="editable.buyer" type="text" class="form-control" id="buyer"
                                aria-describedby="emailHelp">
                        </div>
                        <div class="mb-3 p-3">
                            <label for="street" class="form-label">Street</label>
                            <input v-model="editable.street" type="text" class="form-control" id="street"
                                aria-describedby="emailHelp">
                        </div>
                        <div class="d-flex">
                            <div class="mb-3 w-35 p-3">
                                <label for="city" class="form-label">City</label>
                                <input v-model="editable.city" type="text" class="form-control" id="city">
                            </div>
                            <div class="mb-3 w-25 p-3">
                                <label for="category" class="form-label">State</label>
                                <select v-model="editable.state1" type="text" required class="form-select" id="category"
                                    placeholder="Event Category...">
                                    <option :value="category" v-for="category in categories" :key="category">
                                        {{ category }}
                                    </option>
                                </select>
                            </div>
                            <div class="mb-3 w-25 p-3">
                                <label for="zip" class="form-label">Zip</label>
                                <input v-model="editable.zip" type="text" class="form-control" id="zip">
                            </div>
                        </div>
                        <div class="mb-3 p-3">
                            <label for="total" class="form-label">Total</label>
                            <input v-model="editable.total" type="number" class="form-control" id="total"
                                aria-describedby="emailHelp">
                        </div>
                        <!-- <div class="mb-3 form-check p-3">
                            <input type="checkbox" class="form-check-input" id="exampleCheck1">
                            <label class="form-check-label" for="exampleCheck1">Check me out</label>
                        </div> -->
                        <button type="submit" class="btn btn-outline-dark">Submit</button>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-dark" data-bs-dismiss="modal">Close</button>
                    <button @click="completePurchase()" type="button" class="btn btn-dark">Purchase</button>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { listingsService } from '../services/ListingsService.js';
import { receiptsService } from '../services/ReceiptsService.js';
export default {
    setup() {
        const editable = ref({})
        const categories = ["Florida", "Pennsylvania", "Idaho", "Colorado", "Kansas", "Virginia", "California", "Montana", "Washington", "Oregon", "New Mexico", "Arizona", "Maine", "Ohio", "Indiana"]
        return {
            categories,
            editable,
            receipts: computed(() => AppState.receipts),
            purchases: computed(() => AppState.purchases),
            async completePurchase() {
                try {
                    AppState.purchases = await listingsService.completePurchase()
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            },
            async createReceipt() {
                try {
                    const receiptData = editable.value
                    const receipt = await receiptsService.createReceipt(receiptData)

                    Pop.success('Receipt Created!')
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>