<template>
    <div class="module-destination">
          <span class="title">ĐỊA ĐIỂM NỔI BẬT</span>
          <div class="swiper-button-next swiper-button-next-destination" tabindex="0" role="button"
            aria-label="Next slide">
            <i class="i-arr"></i>
          </div>
          <div class="swiper-button-prev swiper-button-prev-destination" tabindex="0" role="button" aria-label="Previous slide">
            <i class="i-arr"></i>
          </div>
          <div class="swiper-container swiper-destination swiper-container-horizontal">
            <div class="swiper-wrapper">
              <div v-for="(item, index) in items" :key="index" class="swiper-slide swiper-slide-active" :data-swiper-slide-index="index" style="padding: 10px; width: 298.75px; margin-right: 15px;">
                <a class="item-destination" :href="'/find/filter?lat=' + item.loc.lat + '&amp;lng=' + item.loc.lon + '&amp;address=H%E1%BB%93%20Ch%C3%AD%20Minh'">
                  <div class="img-destination">
                    <div class="fix-img">
                      <img :src="item.image" :alt="item.name">
                    </div>
                    <h2>
                      {{item.name}}
                    </h2>
                  </div>
                </a>
              </div>

            </div>
            <span class="swiper-notification" aria-live="assertive" aria-atomic="true"></span>
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
