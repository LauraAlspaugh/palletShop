<template>
    <div class="container-fluid">
        <section class="row justify-content-evenly">
            <div class="col-8">
                <p class="text-center fs-1 p-4">Your Shopping Cart</p>
            </div>
        </section>
        <section v-for="purchase in purchases" :key="purchase.id" class="row justify-content-evenly mb-5">
            <div class="col-3">
                <img class="img-fluid" :src="purchase.listing.img" alt="listing image">
            </div>
            <div class="col-4 listing-name">
                <p class="fs-4">{{ purchase.listing.name }}</p>
            </div>
            <div class="col-4 listing-name">
                <p class="fs-4 p-0">${{ purchase.listing.price }}</p>
            </div>
        </section>
        <section class="row justify-content-evenly">
            <div class="col-10">
                <!-- <p class="fs-3 text-center">Total:{{ purchases.forEach(purchase => purchases.listing.price++) }}
                </p> -->
            </div>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';
import ListingCard from '../components/ListingCard.vue';
export default {
    setup() {
        const calculatedTotal = calculateTotal()
        onMounted(() => {
            getMyPurchases();
            calculateTotal();
        });
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
                const total = 0
                AppState.listings.forEach(listings => {
                    const totalPrice = listings.price * listings.quantity
                    total += totalPrice
                })
                return total
            } catch (error) {
                logger.error(error)
                Pop.error(error)

            }
        }
        return {
            async calculateTotal() {
                try {
                    const total = 0
                    AppState.listings.forEach(listings => {
                        const totalPrice = listings.price * listings.quantity
                        total += totalPrice
                    })
                    return total
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            },
            calculatedTotal,
            account: computed(() => AppState.account),
            purchases: computed(() => AppState.purchases),
            listing: computed(() => AppState.listings)
        };
    },
    components: { ListingCard }
};
</script>


<style lang="scss" scoped>
img {
    height: 300px;
    width: 100%;
    object-fit: cover;
    position: center;
    border: 1px solid black;
}

.listing-name {
    display: flex;
    align-items: center;
    font-family: 'Pinyon Script', cursive;
}
</style>