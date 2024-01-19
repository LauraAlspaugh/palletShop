<template>
    <div class="container-fluid">
        <section class="row">
            <div class="col-12">
                <p class="text-center p-4">Welcome to the shopping page!</p>
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
export default {
    setup() {
        onMounted(() => {
            getListings()
        })
        async function getListings() {
            try {
                await listingsService.getListings()
            } catch (error) {
                logger.error(error)
                Pop.error(error)

            }
        }
        return {
            listings: computed(() => AppState.listings)
        }
    }
};
</script>


<style lang="scss" scoped></style>