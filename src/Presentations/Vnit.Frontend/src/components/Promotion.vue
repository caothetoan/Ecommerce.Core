<template>
  <div class="home-promo">
    <ul class="home-promo-list full-img">
      <li v-for="(item, index) in items" :key="index"  v-if="index < 3">
        <a  :href="'/car/' + item.id" class="item">
          <img :src="item.photos[0].thumbUrl" :alt="item.name" data-size="612">
        </a>
      </li>
    </ul>
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
