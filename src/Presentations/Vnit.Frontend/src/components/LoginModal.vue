<template>
    <div class="modal-content" role="document">
   <div class="modal-header">
      <button type="button" class="close"><span aria-hidden="true">×</span><span class="sr-only"></span></button>
      <h4 class="modal-title">Đăng nhập</h4>
   </div>
   <div class="modal-body">
      <div class="form-default form-s">
         <div class="line-form">
            <div class="wrap-input has-ico"><i class="ic ic-email-fill"></i><input type="text" placeholder="Số điện thoại hoặc email" name="ip_username"></div>
         </div>
         <div class="line-form">
            <div class="wrap-input has-ico"><i class="ic ic-lock-fill"></i><input type="password" placeholder="Mật Khẩu" name="ip_password"></div>
         </div>
         <a class="forgot-password">Quên mật khẩu?</a>
         <div class="clear"></div>
         <a class="btn btn-primary btn--m">Đăng nhập </a>
         <p>Bạn chưa là thành viên? <a href="/signup">Hãy đăng kí ngay!</a></p>
         <span class="line"></span>
         <p class="textAlign-center">Hoặc đăng nhập bằng tài khoản</p>
         <div class="wr-btn-share">
            <div class="wr-btn"><a class="btn btn-facebook btn--m" type="button"><i class="ic ic-facebook"></i> Facebook</a></div>
            <div class="wr-btn"><a class="btn btn-google btn--m" type="button"><i class="ic ic-google"></i> Google</a></div>
         </div>
      </div>
   </div>
</div>
</template>
