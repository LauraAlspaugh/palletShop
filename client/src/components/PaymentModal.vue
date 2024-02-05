<template>
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Stripe Payment(insert third party)</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Enter Address Info.
                    <form>
                        <div class="mb-3 p-3">
                            <label for="street" class="form-label">Street</label>
                            <input type="text" class="form-control" id="text" aria-describedby="emailHelp">
                        </div>
                        <div class="d-flex">
                            <div class="mb-3 w-35 p-3">
                                <label for="city" class="form-label">City</label>
                                <input type="text" class="form-control" id="city">
                            </div>
                            <div class="mb-3 w-25 p-3">
                                <label for="category" class="form-label">State</label>
                                <select v-model="editable.type" type="text" required class="form-select" id="category"
                                    placeholder="Event Category...">
                                    <option :value="category" v-for="category in categories" :key="category">
                                        {{ category }}
                                    </option>
                                </select>
                            </div>
                            <div class="mb-3 w-25 p-3">
                                <label for="zip" class="form-label">Zip</label>
                                <input type="text" class="form-control" id="zip">
                            </div>
                        </div>
                        <div class="mb-3 form-check p-3">
                            <input type="checkbox" class="form-check-input" id="exampleCheck1">
                            <label class="form-check-label" for="exampleCheck1">Check me out</label>
                        </div>
                        <button type="submit" class="btn btn-outline-dark">Submit</button>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-dark" data-bs-dismiss="modal">Close</button>
                    <button @click="completePurchase()" type="button" class="btn btn-dark">Save changes</button>
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
export default {
    setup() {
        const editable = ref({})
        const categories = ["Florida", "Pennsylvania", "Idaho", "Colorado", "Kansas"]
        return {
            categories,
            editable,
            purchases: computed(() => AppState.purchases),
            async completePurchase() {
                try {
                    AppState.myPurchases.listingsService.completePurchase()
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