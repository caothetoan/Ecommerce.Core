<template>
    <div class="not-your-typical bg-promo" :style="'background-image: url(' + items + ')'">
          <div class="wrapper__pr m-container">
            <div class="wrap-promo">
              <h4 class="title-desc">VUI HÈ THÁNG 7</h4>
              <p class="dt-code">Giảm ngay 10% (tối đa 100.000đ/chuyến) khi nhập mã </p>
              <p class="dt-code">Bằng mã code
                <span>MI7</span> từ Mioto </p>
              <p class="note">* Áp dụng đến hết ngày 31/07/2018</p>
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
