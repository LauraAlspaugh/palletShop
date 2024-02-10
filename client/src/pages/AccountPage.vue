<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded-circle profile-pic" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
  <section class="row justify-content-evenly mb-5">
    <p class="text-center fs-4">My Receipts</p>
    <div v-for="receipt in receipts" :key="receipt.id" class="col-12 col-md-6 text-center mt-5 receipt-card">
      <i class="mdi mdi-receipt"></i>
      <p class="fs-4">Ship To:</p>
      <p class="mb-0">{{ receipt.buyer }}</p>
      <p class="mb-0">{{ receipt.street }}</p>
      <span>
        <p>{{ receipt.city }}, {{ receipt.state1 }} {{ receipt.zip }}</p>
      </span>
      <p class="fs-5">Total: ${{ receipt.total }}</p>
      <i title="delete this receipt?" role="button" @click="destroyReceipt(receipt.id)" class="mdi mdi-close"></i>
    </div>
  </section>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { receiptsService } from '../services/ReceiptsService.js';
export default {
  setup() {
    onMounted(() => {
      getReceipts()
    })
    async function getReceipts() {
      try {
        await receiptsService.getReceipts();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    return {
      receipts: computed(() => AppState.receipts),
      account: computed(() => AppState.account),
      async destroyReceipt(receiptId) {
        try {
          const wantsToDelete = await Pop.confirm('Are you sure you want to delete this receipt? ');
          if (!wantsToDelete) {
            return;
          }
          await receiptsService.destroyReceipt(receiptId);
        } catch (error) {
          logger.error(error)
          Pop.error(error)

        }
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.profile-pic {
  border: 1px solid black;
}

.receipt-card {
  width: 40rem;
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

@media (max-width: 900px) {

  .receipt-card {
    width: 20rem;
  }
}
</style>
