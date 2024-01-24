<template>
    <div class="container-fluid">
        <section class="row justify-content-evenly">
            <div class="col-8">
                <p class="text-center fs-1 p-4">Your Shopping Cart</p>
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
export default {
    setup() {
        onMounted(() => {
            getMyPurchases()
        })
        async function getMyPurchases() {
            try {
                await accountService.getMyPurchases()
            } catch (error) {
                logger.error(error)
                Pop.error(error)
            }
        }
        return {}
    }
};
</script>


<style lang="scss" scoped></style>