<template>
    <div class="container-fluid">
        <section class="row">
            <div class="col-12 text-center">
                <p class="fs-4 p-4 cart-name">My Cart</p>
            </div>
        </section>

        <section v-for="purchase in purchases" :key="purchase.id" class="row p-3 mb-3 justify-content-evenly items-list">
            <div class="col-3">
                <img class="img-fluid" :src="purchase.listing.img" alt="listing image">
            </div>

            <div class="col-3 listing-name">
                <p class="p-2">
                    <i @click="destroyPurchase(purchase.id)" class="mdi mdi-minus fs-5" role="button"
                        title="remove this item?"></i>
                </p>
                <p class="fs-5">{{ purchase.listing.name }}</p>
            </div>
            <div class="col-3 pricing-name">
                <p class="fs-4 p-0">${{ purchase.listing.price }}</p>
            </div>
        </section>

        <section class="row justify-content-evenly">
            <div class="col-10 text-center">
                <p class="fs-4 cart-name">Order Summary</p>
                <p>Subtotal ${{ calculatedTotal.toFixed(2) }}</p>
                <p class="fs-5">Total ${{ calculatedTotal.toFixed(2) }}</p>
            </div>
            <div class="text-center mb-3 col-3">
                <button @click="reservePurchases()" data-bs-toggle="modal" data-bs-target="#exampleModal" type="button"
                    class="btn btn-dark w-100 check-button">Checkout</button>
                <p class="p-2"><i class="mdi mdi-shopping fs-5 m-1"></i>Secure Checkout</p>
            </div>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref, watchEffect } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';
import ListingCard from '../components/ListingCard.vue';
import { listingsService } from '../services/ListingsService.js';
export default {
    setup() {
        const calculatedTotal = ref(0)
        onMounted(() => {
            getMyPurchases();

        });
        watchEffect(() => {
            if (AppState.purchases.length > 0) {

                calculateTotal()
            }
        })
        async function getMyPurchases() {
            try {
                await accountService.getMyPurchases();
            }
            catch (error) {
                logger.error(error);
                Pop.error(error);
            }
        }
        async function calculateTotal() {
            try {
                let total = 0
                logger.log("am i reaching this level of total")
                AppState.purchases.forEach(purchase => {
                    const totalPrice = purchase.listing.price
                    total += totalPrice

                })
                calculatedTotal.value = total
            } catch (error) {
                logger.error(error)
                Pop.error(error)

            }
        }
        return {

            calculatedTotal,
            activeListing: computed(() => AppState.activeListing),
            account: computed(() => AppState.account),
            purchases: computed(() => AppState.purchases),
            listing: computed(() => AppState.listings),
            async destroyPurchase(purchaseId) {
                try {
                    const wantstoDestroy = await Pop.confirm('Are you sure you want to remove this Item? ');
                    if (!wantstoDestroy) {
                        return;
                    }
                    // const purchaseId = AppState.purchase
                    const foundPurchase = AppState.purchases.find(purchase => purchase.id == purchaseId)

                    await listingsService.destroyPurchase(purchaseId);
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            },
            async createPurchase() {
                try {
                    if (AppState.listings.quantity == 0) {
                        throw Pop.error("There is insufficient inventory!")
                    }
                    const listingId = route.params.listingId
                    AppState.activeListing.quantity--
                    await listingsService.createPurchase(listingId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            },
            async reservePurchases() {
                try {
                    await accountService.reservePurchases();
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error);
                }
            }
        };
    },
    components: { ListingCard }
};
</script>


<style lang="scss" scoped>
img {
    height: 100px;
    width: 50%;
    object-fit: cover;
    position: center;
    border: 1px solid gray;
}

.listing-name {
    align-items: center;
    display: flex;
}

.pricing-name {
    font-family: 'Pinyon Script', cursive;
    align-items: center;
    display: flex;
}

.items-list {
    border-top: 1px solid gray;
    border-bottom: 1px solid gray;
    width: 100%;
    padding: 5px;
    margin-left: 3px;
    margin-right: 5px;
}

.check-button {
    border-radius: 0px;
}

.cart-name {
    font-family: 'Pinyon Script', cursive;
}
</style>