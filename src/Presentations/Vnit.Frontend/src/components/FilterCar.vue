<template>
<div class="filter-container">
      <div class="content-filter">
         <div class="rent-car has-scroll filter-body">
            <div class="scroll-inner">
               <span class="slstitle">Sắp xếp</span>
               <div class="line-form">
                  <div class="wrap-select">
                     <select>
                        <option value="op">Tối ưu</option>
                        <option value="dt">Ưu tiên khoảng cách</option>
                        <option value="pl">Ưu tiên giá thấp</option>
                        <option value="ph">Ưu tiên giá cao</option>
                        <option value="rt">Ưu tiên đánh giá</option>
                     </select>
                  </div>
               </div>
               <span class="slstitle">Mức giá</span>
               <div class="line-form">
                  <div class="range-slider">
                     <div style="height: 100%; position: relative; cursor: pointer; margin: 0px auto; padding: 8px 0px; width: auto;">
                        <div style="background-color: rgb(185, 185, 185); position: relative; overflow: visible; height: 6px; width: 100%;">
                           <div style="background-color: rgb(0, 165, 80); width: 100%; height: 100%;"></div>
                           <div style="position: absolute; left: 100%; top: 0px; margin-top: -3.5px; margin-left: -6.5px;">
                              <div style="background-color: rgb(20, 20, 20); opacity: 1; border-radius: 100%; height: 13px; width: 13px;"></div>
                           </div>
                        </div>
                     </div>
                     <span class="range-slider__value2" style="float: right; font-size: 12px;">Tất cả giá</span>
                  </div>
               </div>
               <span class="slstitle">Loại xe</span>
               <div class="line-form">
                  <div class="wrap-select">
                     <select>
                        <option value="0">Tất cả</option>
                        <option value="1">4 chỗ (25 xe)</option>
                        <option value="2">7 chỗ (7 xe)</option>
                     </select>
                  </div>
               </div>
               <span class="slstitle">Hãng xe</span>
               <div class="line-form">
                  <div class="wrap-select">
                     <select>
                        <option value="0">Tất cả</option>
                        <option value="1">Audi </option>
                        <option value="12">Bentley </option>
                        <option value="2">BMW </option>
                        <option value="3">Chevrolet (1 xe)</option>
                        <option value="17">Daewoo </option>
                        <option value="4">Ford (1 xe)</option>
                        <option value="10">Honda (2 xe)</option>
                        <option value="11">Hyundai (7 xe)</option>
                        <option value="5">Kia (12 xe)</option>
                        <option value="13">Lexus </option>
                        <option value="20">Luxgen </option>
                        <option value="6">Mazda </option>
                        <option value="7">Mercedes (1 xe)</option>
                        <option value="8">Mitsubishi </option>
                        <option value="14">Nissan (1 xe)</option>
                        <option value="15">Porsche </option>
                        <option value="16">Renault </option>
                        <option value="19">Suzuki (1 xe)</option>
                        <option value="9">Toyota (6 xe)</option>
                        <option value="18">Volkswagen </option>
                     </select>
                  </div>
               </div>
               <a class="func-more block">Nâng cao <i class="i-arrow-down"></i></a>
               <div class="advance-filter hidden">
                  <span class="slstitle">Số chỗ</span>
                  <div class="line-form">
                     <div class="range-slider">
                        <div class="rc-slider">
                           <div class="rc-slider-rail" style="background: rgb(185, 185, 185); height: 6px; border-radius: 0px; cursor: pointer;"></div>
                           <div class="rc-slider-track rc-slider-track-1" style="background: rgb(0, 165, 80); height: 6px; cursor: pointer; left: 0%; width: 100%;"></div>
                           <div class="rc-slider-step"></div>
                           <div role="slider" tabindex="0" aria-valuemin="2" aria-valuemax="16" aria-valuenow="2" aria-disabled="false" class="rc-slider-handle rc-slider-handle-1" style="background: rgb(20, 20, 20); border: none; cursor: pointer; box-shadow: none; left: 0%;"></div>
                           <div role="slider" tabindex="0" aria-valuemin="2" aria-valuemax="16" aria-valuenow="16" aria-disabled="false" class="rc-slider-handle rc-slider-handle-2" style="background: rgb(20, 20, 20); border: none; cursor: pointer; box-shadow: none; left: 100%;"></div>
                           <div class="rc-slider-mark"></div>
                        </div>
                        <span class="range-slider__value2" style="padding-top: 5px; float: right; font-size: 12px;">Bất kì</span>
                     </div>
                  </div>
                  <span class="slstitle">Năm sản xuất</span>
                  <div class="line-form">
                     <div class="range-slider">
                        <div class="rc-slider">
                           <div class="rc-slider-rail" style="background: rgb(185, 185, 185); height: 6px; border-radius: 0px; cursor: pointer;"></div>
                           <div class="rc-slider-track rc-slider-track-1" style="background: rgb(0, 165, 80); height: 6px; cursor: pointer; left: 0%; width: 100%;"></div>
                           <div class="rc-slider-step"></div>
                           <div role="slider" tabindex="0" aria-valuemin="2000" aria-valuemax="2018" aria-valuenow="2000" aria-disabled="false" class="rc-slider-handle rc-slider-handle-1" style="background: rgb(20, 20, 20); border: none; cursor: pointer; box-shadow: none; left: 0%;"></div>
                           <div role="slider" tabindex="0" aria-valuemin="2000" aria-valuemax="2018" aria-valuenow="2018" aria-disabled="false" class="rc-slider-handle rc-slider-handle-2" style="background: rgb(20, 20, 20); border: none; cursor: pointer; box-shadow: none; left: 100%;"></div>
                           <div class="rc-slider-mark"></div>
                        </div>
                        <span class="range-slider__value2" style="padding-top: 5px; float: right; font-size: 12px;">Bất kì</span>
                     </div>
                  </div>
                  <span class="slstitle">Giao dịch</span>
                  <div class="line-form">
                     <div class="squaredFour have-label"><input id="ip_instant_booking" type="checkbox"><label for="ip_instant_booking">Cho thuê ngay</label></div>
                     <div class="squaredFour have-label"><input id="ip_direct_delivery" type="checkbox"><label for="ip_direct_delivery">Giao xe tận nơi</label></div>
                  </div>
                  <span class="slstitle">Truyền động</span>
                  <div class="line-form">
                     <div class="wrap-select">
                        <select>
                           <option value="0">Tất cả</option>
                           <option value="1">Số tự động</option>
                           <option value="2">Số sàn</option>
                        </select>
                     </div>
                  </div>
                  <span class="slstitle">Nhiên liệu</span>
                  <div class="line-form">
                     <div class="wrap-select">
                        <select>
                           <option value="0">Tất cả</option>
                           <option value="1">Xăng</option>
                           <option value="2">Dầu</option>
                        </select>
                     </div>
                  </div>
                  <span class="slstitle">Mức tiêu thụ nhiên liệu</span>
                  <div class="line-form">
                     <div class="range-slider">
                        <div style="height: 100%; position: relative; cursor: pointer; margin: 0px auto; padding: 8px 0px; width: auto;">
                           <div style="background-color: rgb(185, 185, 185); position: relative; overflow: visible; height: 6px; width: 100%;">
                              <div style="background-color: rgb(0, 165, 80); width: 0%; height: 100%;"></div>
                              <div style="position: absolute; left: 0%; top: 0px; margin-top: -3.5px; margin-left: -6.5px;">
                                 <div style="background-color: rgb(20, 20, 20); opacity: 1; border-radius: 100%; height: 13px; width: 13px;"></div>
                              </div>
                           </div>
                        </div>
                        <span class="range-slider__value2" style="float: right; font-size: 12px;">Bất kì</span>
                     </div>
                  </div>
                  <span class="slstitle">Tính năng</span>
                  <div class="line-form">
                     <div>
                        <div class="squaredFour have-label">
                           <div class="squaredFour have-label"><input id="cb-feature-sr" type="checkbox" value="sr"><label for="cb-feature-sr"> <img class="img-ico" src="https://n1-cstg.mioto.vn/g/2017/11/04/17/34.jpg" alt="Mioto - Thuê xe tự lái" style="width: 16px; height: 16px;"> Cửa sổ trời</label></div>
                        </div>
                        <div class="squaredFour have-label">
                           <div class="squaredFour have-label"><input id="cb-feature-bt" type="checkbox" value="bt"><label for="cb-feature-bt"> <img class="img-ico" src="https://n1-cstg.mioto.vn/g/2017/11/04/17/33.jpg" alt="Mioto - Thuê xe tự lái" style="width: 16px; height: 16px;"> Bluetooth</label></div>
                        </div>
                        <div class="squaredFour have-label">
                           <div class="squaredFour have-label"><input id="cb-feature-gp" type="checkbox" value="gp"><label for="cb-feature-gp"> <img class="img-ico" src="https://n1-cstg.mioto.vn/g/2017/11/04/17/37.jpg" alt="Mioto - Thuê xe tự lái" style="width: 16px; height: 16px;"> Định vị GPS</label></div>
                        </div>
                        <div class="squaredFour have-label">
                           <div class="squaredFour have-label"><input id="cb-feature-us" type="checkbox" value="us"><label for="cb-feature-us"> <img class="img-ico" src="https://n1-cstg.mioto.vn/g/2017/11/04/17/36.jpg" alt="Mioto - Thuê xe tự lái" style="width: 16px; height: 16px;"> Khe cắm USB</label></div>
                        </div>
                        <div class="squaredFour have-label">
                           <div class="squaredFour have-label"><input id="cb-feature-bs" type="checkbox" value="bs"><label for="cb-feature-bs"> <img class="img-ico" src="https://n1-cstg.mioto.vn/g/2017/11/04/17/35.jpg" alt="Mioto - Thuê xe tự lái" style="width: 16px; height: 16px;"> Ghế trẻ em</label></div>
                        </div>
                     </div>
                  </div>
                  <span class="slstitle">Giới hạn quãng đường</span>
                  <div class="line-form">
                     <div class="range-slider">
                        <div style="height: 100%; position: relative; cursor: pointer; margin: 0px auto; padding: 8px 0px; width: auto;">
                           <div style="background-color: rgb(185, 185, 185); position: relative; overflow: visible; height: 6px; width: 100%;">
                              <div style="background-color: rgb(0, 165, 80); width: 100%; height: 100%;"></div>
                              <div style="position: absolute; left: 100%; top: 0px; margin-top: -3.5px; margin-left: -6.5px;">
                                 <div style="background-color: rgb(20, 20, 20); opacity: 1; border-radius: 100%; height: 13px; width: 13px;"></div>
                              </div>
                           </div>
                        </div>
                        <span class="range-slider__value2" style="float: right; font-size: 12px;">Bất kì</span>
                     </div>
                  </div>
                  <span class="slstitle">Phí vượt giới hạn</span>
                  <div class="line-form">
                     <div class="range-slider">
                        <div style="height: 100%; position: relative; cursor: pointer; margin: 0px auto; padding: 8px 0px; width: auto;">
                           <div style="background-color: rgb(185, 185, 185); position: relative; overflow: visible; height: 6px; width: 100%;">
                              <div style="background-color: rgb(0, 165, 80); width: 100%; height: 100%;"></div>
                              <div style="position: absolute; left: 100%; top: 0px; margin-top: -3.5px; margin-left: -6.5px;">
                                 <div style="background-color: rgb(20, 20, 20); opacity: 1; border-radius: 100%; height: 13px; width: 13px;"></div>
                              </div>
                           </div>
                        </div>
                        <span class="range-slider__value2" style="float: right; font-size: 12px;">Bất kì</span>
                     </div>
                  </div>
                  <span class="slstitle">Khoảng cách</span>
                  <div class="line-form">
                     <div class="range-slider">
                        <div style="height: 100%; position: relative; cursor: pointer; margin: 0px auto; padding: 8px 0px; width: auto;">
                           <div style="background-color: rgb(185, 185, 185); position: relative; overflow: visible; height: 6px; width: 100%;">
                              <div style="background-color: rgb(0, 165, 80); width: 100%; height: 100%;"></div>
                              <div style="position: absolute; left: 100%; top: 0px; margin-top: -3.5px; margin-left: -6.5px;">
                                 <div style="background-color: rgb(20, 20, 20); opacity: 1; border-radius: 100%; height: 13px; width: 13px;"></div>
                              </div>
                           </div>
                        </div>
                        <span class="range-slider__value2" style="float: right; font-size: 12px;">Bất kì</span>
                     </div>
                  </div>
                  <span class="slstitle">Hình ảnh</span>
                  <div class="line-form">
                     <div class="squaredFour have-label"><input id="ip_photos_verified" type="checkbox"><label for="ip_photos_verified">Đã xác thực</label></div>
                  </div>
               </div>
               <div class="space m"></div>
               <div class="wrap-btn-filter"><a class="btn btn-primary btn--m hide-on-med-and-down" href="/find/filter?startDate=1532095200000&amp;endDate=1532178000000&amp;address=Duy Tân, Cầu Giấy, Hà Nội, Việt Nam">Làm mới</a><a class="func-reset"><i class="ic ic-reset"></i></a></div>
            </div>
         </div>
      </div>
   </div>
</template>
<script>
export default {
}
</script>

