<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <PaymentModal />
  </main>
  <footer class=" text-dark">
    <p class="m-5 fs-5"><i class="mdi mdi-copyright"></i> 2024 by Laura Alspaugh.</p>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import PaymentModal from './components/PaymentModal.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, PaymentModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
  background-color: white;
}
</style>
