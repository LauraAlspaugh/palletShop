<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
  <section class="row justify-content-evenly">
    <div v-for="receipt in receipts" :key="receipt.id" class="col-4 text-center receipt-card">
      <p class="fs-5">Ship To:</p>
      <p class="mb-0">{{ receipt.buyer }}</p>
      <p class="mb-0">{{ receipt.street }}</p>
      <span>
        <p>{{ receipt.city }}, {{ receipt.state1 }} {{ receipt.zip }}</p>
      </span>
      <p>Total: ${{ receipt.total }}</p>
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
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.receipt-card {
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
</style>
