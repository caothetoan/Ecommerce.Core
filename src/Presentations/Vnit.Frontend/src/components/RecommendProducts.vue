<template>
  <div class="home-star home-sec">
    <div class="sec-hd">
      <h2 class="sec-title">SẢN PHẨM HOT</h2>
    </div>
    <div class="sec-bd">
      <ul class="home-star-list list-4 full-img">
        <li v-for="(item, index) in items" :key="index"  v-if="index"
        < 4">
          <a :href="'/product/' + item.id" class="item">
          <img :src="item.photos[0].thumbUrl" :alt="item.name" data-size="612">
        </a>
        </li>
      </ul>
    </div>
  </div>
</template>
<script>
  export default {
  props: {
  title: "",
  items: "",
  pagination: ""
  },
  }
</script>
