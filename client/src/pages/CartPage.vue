<template>
    <div class="container-fluid">
        <section class="row justify-content-start">
            <div class="col-2">
                <p class="fs-2 p-4 mr-3">My Cart</p>
            </div>
        </section>
        <section v-for="purchase in purchases" :key="purchase.id" class="row d-flex mb-3 justify-content-center items-list">
            <!-- <div v-for="purchase in purchases" :key="purchase.id"> -->
            <div class="col-2">
                <img class="img-fluid" :src="purchase.listing.img" alt="listing image">
            </div>

            <div class="col-3 listing-name">
                <p class="fs-4">{{ purchase.listing.name }}</p>
            </div>
            <div class="col-3 listing-name">
                <p class="fs-4 p-0">${{ purchase.listing.price }}</p>
            </div>
            <!-- </div> -->
        </section>
        <section v-for="purchase in purchases" :key="purchase.id" class="row justify-content-evenly mb-5">
        </section>
        <section class="row justify-content-evenly">
            <div class="col-10">
                <!-- <p class="fs-3 text-center">Total:{{ purchases.forEach(purchase => purchases.listing.price++) }}
                </p> -->
            </div>
            <div class="text-center mb-3">

                <button type="button" class="btn btn-dark">Checkout</button>
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
    height: 100px;
    width: 80%;
    object-fit: cover;
    position: center;
    border: 1px solid black;
}

.listing-name {
    font-family: 'Pinyon Script', cursive;
    align-items: center;
    display: flex;
}

.items-list {
    border-top: 1px solid gray;
    border-bottom: 1px solid gray;
    width: 60%;
    padding: 5px;
}
</style>