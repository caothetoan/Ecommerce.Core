<template>
  <div class="home-star home-sec">
    <div class="sec-hd">
      <h2 class="sec-title">SẢN PHẨM MỚI</h2>
    </div>
    <div class="sec-bd">
      <div class="m-product-list h-product-list1 clearfix">
     
        <div v-for="(item, index) in items" :key="index" class="pro-item m-tag-a first" data-target="_blank">
            <div class="pro-img m-img-hover">
              <a :href="'/car/' + item.id" class="item">
                <img :src="item.photos[0].thumbUrl" :alt="item.name" data-size="612"/>
              </a>
            </div>
            <div class="pro-tags-con"></div>
            <p class="pro-desc">20厘米厚度，独立袋装弹簧...</p>
            <div class="h-text-inner">
              <p class="pro-info">8H乳胶弹簧静音床垫M3</p>
              <p class="pro-price">
                <span class="pro-unit">¥</span>
                <span class="m-num">1499</span>
                <span class="pro-flag">起</span>
                <span class="m-sale-tag">直降</span>
              </p>
            </div>
          </div>
      </div>
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
