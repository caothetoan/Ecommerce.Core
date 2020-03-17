<template>
      <div class="module-deal">
          <div class="space m"></div>
          <div class="space m"></div>
          <span class="title">XE MỚI ĐĂNG KÍ</span>
          <div class="swiper-button-next swiper-button-next-new-box" tabindex="0" role="button" aria-label="Next slide"
            aria-disabled="false">
            <i class="i-arr"></i>
          </div>
          <div class="swiper-button-prev swiper-button-prev-new-box swiper-button-disabled" tabindex="0" role="button" aria-label="Previous slide"
            aria-disabled="true">
            <i class="i-arr"></i>
          </div>
          <div class="swiper-container swiper-new swiper-container-horizontal">
            <div class="swiper-wrapper" style="transform: translate3d(0px, 0px, 0px);">

              <div v-for="(item, index) in items" :key="index"  class="swiper-slide swiper-slide-active" style="width: 292px; margin-right: 24px;">
                <div class="item-car">
                  <a :href="'/car/' + item.id">
                    <span class="img-car">
                       <div class="fix-img">
                        <img :src="item.photos[0].thumbUrl" :alt="item.name">
                      </div>
                    </span>
                    <div class="desc-car">
                      <div class="star-ratings" title="0 Stars" style="position: relative; box-sizing: border-box; display: inline-block;">
                        <svg class="star-grad" style="position: absolute; z-index: 0; width: 0px; height: 0px; visibility: hidden;">
                          <defs>
                            <lineargradient id="starGrad887299437078356" x1="0%" y1="0%" x2="100%" y2="0%">
                              <stop offset="0%" class="stop-color-first" style="stop-color: rgb(0, 165, 80); stop-opacity: 1;"></stop>
                              <stop offset="0%" class="stop-color-first" style="stop-color: rgb(0, 165, 80); stop-opacity: 1;"></stop>
                              <stop offset="0%" class="stop-color-final" style="stop-color: rgb(203, 211, 227); stop-opacity: 1;"></stop>
                              <stop offset="100%" class="stop-color-final" style="stop-color: rgb(203, 211, 227); stop-opacity: 1;"></stop>
                            </lineargradient>
                          </defs>
                        </svg>
                        <div class="star-container" style="position: relative; display: inline-block; vertical-align: middle; padding-right: 1px;">
                          <svg viewBox="0 0 51 48" class="widget-svg" style="width: 17px; height: 17px; transition: transform 0.2s ease-in-out;">
                            <path class="star" d="m25,1 6,17h18l-14,11 5,17-15-10-15,10 5-17-14-11h18z" style="fill: rgb(203, 211, 227); transition: fill 0.2s ease-in-out;"></path>
                          </svg>
                        </div>
                        <div class="star-container" style="position: relative; display: inline-block; vertical-align: middle; padding-left: 1px; padding-right: 1px;">
                          <svg viewBox="0 0 51 48" class="widget-svg" style="width: 17px; height: 17px; transition: transform 0.2s ease-in-out;">
                            <path class="star" d="m25,1 6,17h18l-14,11 5,17-15-10-15,10 5-17-14-11h18z" style="fill: rgb(203, 211, 227); transition: fill 0.2s ease-in-out;"></path>
                          </svg>
                        </div>
                        <div class="star-container" style="position: relative; display: inline-block; vertical-align: middle; padding-left: 1px; padding-right: 1px;">
                          <svg viewBox="0 0 51 48" class="widget-svg" style="width: 17px; height: 17px; transition: transform 0.2s ease-in-out;">
                            <path class="star" d="m25,1 6,17h18l-14,11 5,17-15-10-15,10 5-17-14-11h18z" style="fill: rgb(203, 211, 227); transition: fill 0.2s ease-in-out;"></path>
                          </svg>
                        </div>
                        <div class="star-container" style="position: relative; display: inline-block; vertical-align: middle; padding-left: 1px; padding-right: 1px;">
                          <svg viewBox="0 0 51 48" class="widget-svg" style="width: 17px; height: 17px; transition: transform 0.2s ease-in-out;">
                            <path class="star" d="m25,1 6,17h18l-14,11 5,17-15-10-15,10 5-17-14-11h18z" style="fill: rgb(203, 211, 227); transition: fill 0.2s ease-in-out;"></path>
                          </svg>
                        </div>
                        <div class="star-container" style="position: relative; display: inline-block; vertical-align: middle; padding-left: 1px;">
                          <svg viewBox="0 0 51 48" class="widget-svg" style="width: 17px; height: 17px; transition: transform 0.2s ease-in-out;">
                            <path class="star" d="m25,1 6,17h18l-14,11 5,17-15-10-15,10 5-17-14-11h18z" style="fill: rgb(203, 211, 227); transition: fill 0.2s ease-in-out;"></path>
                          </svg>
                        </div>
                      </div>
                      <h2>{{item.name}}</h2>
                      <p>
                        <span>
                          <i class="ic ic-clock"></i> {{item.totalTrips}} chuyến</span>
                        <span>
                          <i class="ic ic-tag"></i> {{item.price}}</span>
                      </p>
                    </div>
                  </a>
                </div>
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
