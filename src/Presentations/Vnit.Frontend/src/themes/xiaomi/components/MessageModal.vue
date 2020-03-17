<template>
    <div>
      <div class="modal hide fade xm-dm-queue" id="xmDmQueue">
        <a type="button" data-dismiss="modal" aria-hidden="true" class="modalclose iconfont">&#xe92b;</a>
        <div class="modal-body">
          <h3>Bạn đang xếp hàng...</h3>
          <p class="queue-tip">Hiện tại có quá nhiều người, vui lòng chờ giây lát.</p>
          <div class="queue-animate">
            <div id="mituWalking" class="mitu-walk"></div>
            <div class="animate-mask animate-mask-left"></div>
            <div class="animate-mask animate-mask-right"></div>
          </div>
        </div>
      </div>
      <div id="xmDmError" class="modal hide fade xm-dm-error">
        <a type="button" data-dismiss="modal" aria-hidden="true" class="modalclose iconfont">&#xe92b;</a>
        <div class="modal-body">
          <h3>Ộp! Chúng ta đang bị kẹt!</h3>
          <p class="error-tip">Quá đông người, hãy thử lại sau</p>
          <p>
            <a class="btn btn-orange" id="xmDmReload">Thử lại</a>
          </p>
        </div>
      </div>

    </div>
</template>
<script>
export default {
};
</script>

