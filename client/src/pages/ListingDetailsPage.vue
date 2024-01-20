<template>
    <div class="container-fluid mt-5">
        <section v-if="listing" class="row justify-content-center">
            <div class="col-4">
                <img class="img-fluid" :src="listing.img" alt="image of listing">
            </div>
            <div class="col-4">
                <p class="p-0 mt-5 fs-5">{{ listing.name }}</p>
                <p>{{ listing.description }}</p>
                <p>${{ listing.price }}</p>
                <p>{{ listing.quantity }} in stock</p>
            </div>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { listingsService } from '../services/ListingsService.js';
import { useRoute } from 'vue-router';
export default {
    setup() {
        onMounted(() => {
            getListingById()
        })
        const route = useRoute()
        async function getListingById() {
            try {
                const listingId = route.params.listingId;
                await listingsService.getListingById(listingId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error);
            }
        }
        return {
            listing: computed(() => AppState.activeListing)
        }
    }
};
</script>


<style lang="scss" scoped>
img {
    height: 500px;
    width: 100%;
    object-fit: cover;
    position: center;
}
</style>