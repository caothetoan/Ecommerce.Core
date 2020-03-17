<template>
  <div class="home-top-slides" id="topSlide">
   
    <div class='slider' v-for="(item, index) in items" :key="index">
      <a :href="'/product/' + item.id">
        <img class="slide-image"  :src="item.photos[0].thumbUrl" :alt="item.name" data-size="612" />
      </a>
      <div>
        <div class="txt c-black" data-color="c-black">
          <h3 class='title'></h3>
          <p class='descp'></p>
          <p class="price">
            <span class="dollar"></span>
          </p>
        </div>
      </div>
    </div>

  </div>
  
</template>
<script>
export default {
 props: {
    title: "",
    items: ""
  },
}
</script>
