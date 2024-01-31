<template>
    <div class="container-fluid">
        <div class="section-row mb-3 p-4">
            <div class=" md-12 col-md-12 mt-3" v-if="!isMobile">

                <div class="d-flex rounded-pill justify-content-around">
                    <button class="btn btn-outline-dark w-100 mx-3" @click="changeCategory('')">All</button>
                    <button class="btn btn-outline-dark  w-100 mx-3" @click="changeCategory(category)"
                        v-for="category in categories" :key="category">
                        {{ category }}
                    </button>
                </div>
            </div>
            <div class="dropdown" v-else>
                <button class="dropdown-toggle" data-bs-toggle="dropdown" role="button" aria-expanded="false">
                    Dropdown</button>
                <ul class="dropdown-menu">
                    <li @click="changeCategory('')" class="dropdown-item">All</li>
                    <li @click="changeCategory(category)" v-for="category in categories" :key="category"
                        class="dropdown-item">
                        {{ category }}</li>
                </ul>
            </div>
        </div>
        <section class="row">
            <div v-for="listing in listings" :key="listing.id" class="col-12 col-md-4  d-flex justify-content-center">
                <ListingCard :listingProp="listing" />
            </div>
        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { listingsService } from '../services/ListingsService.js';
import ListingCard from '../components/ListingCard.vue';
export default {
    setup() {
        const categories = ["Tech", "Decor", "Furniture", "Tools"];
        const filteredCategory = ref("");
        onMounted(() => {
            getListings();
        });
        async function getListings() {
            try {


                await listingsService.getListings();

            }
            catch (error) {
                logger.error(error);
                Pop.error(error);
            }
        }
        return {
            categories,
            filteredCategory,
            isMobile: computed(() => {
                let isMobile = false
                if (window.innerWidth < 768) {
                    isMobile = true
                }
                return isMobile
            }),

            listings: computed(() => {
                if (filteredCategory.value) {
                    return AppState.listings.filter((listing) => listing.category == filteredCategory.value);

                } else {
                    return AppState.listings
                }
            }),
            changeCategory(category) {
                logger.log(category)
                filteredCategory.value = category
            }
        };
    },
    components: { ListingCard }
};
</script>


<style lang="scss" scoped></style>