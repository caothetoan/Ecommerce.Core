<template>
  <div class="home-media home-sec">
    <div class="sec-hd">
      <h2 class="sec-title">TIN TỨC</h2>
    </div>
    <div class="sec-bd">
      <div class="home-media-slides" id="mediaSlide">

        <div class='slider' v-for="(item, index) in items" :key="index" >
          <img :src="item.image" :alt="item.name" />
          <div class="container">
            <div class='txt'>
              <h3 class='title'>
                {{item.name}}
              </h3>
              <p class='descp'>Mi MIX 2 kế thừa thiết kế độc đáo của đời đầu như màn hình tràn 3 cạnh, camera trước bố trí ở phần dưới, viền camera mạ vàng 18K.</p>
              <a :href="'/newsItem/' + item.id" target="_blank"  class="more">Xem thêm</a>
              <p class="media-name">Tạp chí Nghe Nhìn Việt Nam</p>
            </div>
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
