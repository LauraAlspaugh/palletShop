<template>
    <div class="container-fluid">
        <section class="row justify-content-evenly">
            <div class="col-8">
                <p class="text-center fs-1 p-4">Your Shopping Cart</p>
            </div>
        </section>
        <section class="row justify-content-evenly">
            <div v-for="purchase in purchases" :key="purchase.id" class="col-8">
                <ListingCard />
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
            purchases: computed(() => AppState.myPurchases),
            listings: computed(() => AppState.listings)
        };
    },
    components: { ListingCard }
};
</script>


<style lang="scss" scoped></style>