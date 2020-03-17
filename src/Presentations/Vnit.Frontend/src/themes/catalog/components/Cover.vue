<template>
      <div class="cover">
          <div class="slogan">
            <h1>Thuê xe tự lái - Tải ngay Mioto!</h1>
          </div>
          <div class="cover-container" :style="'background-image: url(' + items + ')'">
            <div class="inside form-default">
              <div class="rent-car">
                <div class="lstitle">
                  <i class="ic ic-car"></i>
                  <span>Bạn cần thuê xe?</span>
                </div>
                <div class="line-form">
                  <label class="label">Địa điểm</label>
                  <div class="wrap-input has-ico ">
                    <i class="ic ic-location-f"></i>
                    <div id="PlacesAutocomplete__root" class="" style="position: relative; padding-bottom: 0px;">
                      <input type="text" autocomplete="off" placeholder="Nhập thành phố, quận, địa chỉ..." class="" value="Duy Tân, Cầu Giấy, Hà Nội, Việt Nam">
                    </div>
                  </div>
                </div>
                <div class="line-form has-timer">
                  <label class="label">Thời gian</label>
                  <div class="wrap-input">
                    <div class="react-bootstrap-daterangepicker-container" style="display: inline-block;">
                      <span style="font-size: 13px; width: 100%; padding: 13px 20px; display: inline-block; cursor: pointer;">21:00 19/07/2018 - 20:00 20/07/2018</span>
                    </div>
                  </div>
                </div>
                <div class="line-action">
                  <a class="cta-main" href="https://www.mioto.vn/find/filter?startDate=1532008800000&amp;endDate=1532091600000&amp;address=Duy%20T%C3%A2n,%20C%E1%BA%A7u%20Gi%E1%BA%A5y,%20H%C3%A0%20N%E1%BB%99i,%20Vi%E1%BB%87t%20Nam">TÌM XE NGAY</a>
                  <i class="i-arr"></i>
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
    items: ""
  },
}
</script>