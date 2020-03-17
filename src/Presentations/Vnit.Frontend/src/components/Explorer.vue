<template>
    <div>
      <div id="home_videowrap" class="modal modal-video fade">
        <div class="modal-header">
          <a type="button" data-dismiss="modal" aria-hidden="true" class="modalclose iconfont">&#xe92b;</a>
          <h3 class="title"> </h3>
        </div>
        <div class="modal-body"></div>
      </div>
      <div id="slideVideoPop" class="modal modal-slide-video fade">
        <a type="button" data-dismiss="modal" aria-hidden="true" class="modalclose iconfont">&#xe92b;</a>
        <div class="modal-body"></div>
        <img src="//i01.appmifile.com/webfile/globalimg/hd/2017011301/c6-video-bg.jpg" class="c6-bg J_slidePopImg" alt=""/>
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
