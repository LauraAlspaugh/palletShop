<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12">
      </div>
    </section>
    <section class="row justify-content-evenly mt-5">
      <div class="col-11">
        <div class="main-image"></div>
        <div class="head-title">
          <p class="fs-1">Products at a Cheaper Cost.</p>
          <RouterLink :to="{ name: 'Items' }">
            <button class="btn btn-outline-white">Shop Now</button>
          </RouterLink>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  setup() {
    return {

    }
  }
}
</script>

<style scoped lang="scss">
.main-image {
  background-image: url("https://images.unsplash.com/photo-1633599925393-a4af0a650546?q=80&w=2940&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
  background-position: center;
  background-size: cover;
  background-repeat: no-repeat;
  height: 500px;
  border-radius: 7px;
  filter: blur(4px);
  -webkit-filter: blur(4px);
}

.head-title {
  background-color: rgb(0, 0, 0);
  /* Fallback color */
  background-color: rgba(0, 0, 0, 0.4);
  /* Black w/opacity/see-through */
  color: #B2BABB;
  font-weight: bold;
  border: 3px solid #B2BABB;
  border-radius: 7px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 2;
  width: 80%;
  padding: 20px;
  text-align: center;
}
</style>
