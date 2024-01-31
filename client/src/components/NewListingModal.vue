<template>
    <div class="modal fade" id="createListingModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-dark" id="exampleModalLabel">Create a Listing</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-dark">
                    <form @submit.prevent="createListing()">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input v-model="editable.name" type="text" required class="form-control" id="name"
                                placeholder="Listing Name...">

                        </div>
                        <div class="mb-3">
                            <label for="imageUrl" class="form-label">Image</label>
                            <input v-model="editable.img" type="url" required class="form-control" id="imgUrl"
                                placeholder="Listing Image Url...">
                        </div>
                        <div class="mb-3">
                            <label for="description" class="form-label">Description</label>
                            <textarea v-model="editable.description" rows="5" type="text-area" required class="form-control"
                                id="description" placeholder="Listing Description..."></textarea>

                        </div>
                        <div class="mb-3">
                            <label for="price" class="form-label">Price</label>
                            <input v-model="editable.price" type="text" required class="form-control" id="price"
                                placeholder="Listing Price...">

                        </div>
                        <div class="mb-3">
                            <label for="quantity" class="form-label">Quantity</label>
                            <input v-model="editable.quantity" type="text" required class="form-control" id="quantity"
                                placeholder="Listing Quantity...">

                        </div>
                        <div class="mb-3">
                            <label for="category" class="form-label">Category</label>
                            <select v-model="editable.category" type="text" required class="form-select" id="category"
                                placeholder="Listing Category...">
                                <option :value="category" v-for="category in categories" :key="category">
                                    {{ category }}
                                </option>
                            </select>
                        </div>


                        <button type="submit" class="btn btn-outline-dark">Submit</button>
                    </form>

                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { listingsService } from '../services/ListingsService.js';
import Pop from '../utils/Pop.js';
import { Modal } from 'bootstrap';
import { logger } from '../utils/Logger.js';
export default {
    setup() {
        const categories = ["Tech", "Decor", "Tools", "Furniture"]
        const editable = ref({})
        const router = useRouter()
        return {
            categories,
            editable,
            listings: computed(() => AppState.listings),
            async createListing() {
                try {
                    const listingData = editable.value
                    const listing = await listingsService.createListing(listingData)
                    Pop.success('Listing Created!')
                    editable.value = {}
                    Modal.getOrCreateInstance("#createListingModal").hide()
                    router.push({ name: "Listing", params: { listingId: listing.id } })
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
.modal-footer {
    background-color: #7F8C8D;

}

.modal-header {
    background-color: #7F8C8D;
    font-family: 'Pinyon Script', cursive;
    color: white;
}
</style>