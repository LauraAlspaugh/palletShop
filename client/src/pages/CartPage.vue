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
        onMounted(() => {
            getMyPurchases();
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
        return {
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