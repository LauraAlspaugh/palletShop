<template>
    <div class="container-fluid mt-5">
        <section v-if="listing" class="row justify-content-center">
            <div class="col-12 col-md-4">
                <img class="img-fluid" :src="listing.img" alt="image of listing">
            </div>
            <div class="col-12 col-md-4 second-column">
                <p class="p-0 mt-5 fs-3 listing-name">{{ listing.name }}</p>
                <p>{{ listing.description }}</p>
                <p class="price-name fs-5">${{ listing.price }}.00</p>
                <p>{{ listing.quantity }} in stock</p>
                <div class="mb-3 quantity-box">
                    <label for="quantity" class="form-label fs-5">Quantity</label>
                    <input type="text" class="form-control" id="quantity" required>
                </div>
                <button @click="createPurchase()" class="btn btn-outline-dark mb-3">Add to Cart</button>
                <RouterLink :to="{ name: 'Cart' }">
                    <button @click="createPurchase()" class="btn btn-dark text-white buy-button mb-3">Buy Now</button>
                </RouterLink>
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
            listing: computed(() => AppState.activeListing),
            async createPurchase() {
                try {
                    if (AppState.activeListing.quantity == 0) {
                        throw Pop.error("There is insufficient inventory!")
                    }
                    const listingId = route.params.listingId
                    // AppState.activeListing.quantity--
                    await listingsService.createPurchase(listingId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            }
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
    border: solid black 2px;
}

.listing-name {
    font-family: 'Pinyon Script', cursive;
    color: #7F8C8D;
}

button {
    width: 80%;
}

input {
    width: 20%;
    border: black solid 2px;

}

label {
    font-family: 'Pinyon Script', cursive;
    color: #7F8C8D;
}

.price-name {
    font-family: 'Pinyon Script', cursive;
}

@media (max-width: 900px) {

    .second-column {
        text-align: center;
    }

    input {
        width: 100%;
    }
}
</style>