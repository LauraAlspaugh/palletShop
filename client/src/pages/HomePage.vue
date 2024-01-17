<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12">

      </div>
    </section>
    <section class="row justify-content-evenly mt-5">
      <div class="col-11 main-image">
        <p class="fs-1 text-center text-light head-title">Products at a Cheaper Cost.</p>
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


}

.head-title {
  vertical-align: center;
  align-items: center;
}
</style>
