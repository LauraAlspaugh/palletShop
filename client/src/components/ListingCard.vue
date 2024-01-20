<template>
    <div class="listing-card mb-5">
        <router-link :to="{ name: 'Listing', params: { listingId: listingProp.id } }">
            <img class="img-fluid" :src="listingProp.img" alt="photo of listing">
        </router-link>
        <p class="fs-4 text-center mt-2 listing-name">{{ listingProp.name }}</p>
        <p class="listing-name text-center fs-4">${{ listingProp.price }}</p>
        <!-- <p class="text-center"><i class="mdi mdi-eye text-center"></i></p> -->
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Listing } from '../models/Listing.js';

export default {
    props: { listingProp: { type: Listing, required: true } },
    setup() {
        return {
            listings: computed(() => AppState.listings)
        }
    }
};
</script>


<style lang="scss" scoped>
.listing-card {
    width: 22rem;
    border: 2px solid #7F8C8D;
    padding: 5px;
    border-radius: 7px;
    /* From https://css.glass */
    background: rgba(187, 174, 174, 0.62);
    border-radius: 16px;
    box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
    backdrop-filter: blur(3.6px);
    -webkit-backdrop-filter: blur(3.6px);
    border: 1px solid #7F8C8D(30, 23, 23, 0.78);
}

img {
    border-radius: 7px;
    width: 22rem;
    height: 18rem;
    object-fit: cover;
    position: center;
    ;
}

.listing-name {
    font-family: 'Pinyon Script', cursive;
    color: #7F8C8D;
    text-shadow: 2px 1px 2px rgb(87, 82, 82);
}
</style>