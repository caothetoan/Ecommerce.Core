<template>
  <section>
      <div>
        <div>
          <div class="bm-overlay" style="position: fixed; z-index: 1000; width: 100%; height: 100%; background: rgba(0, 0, 0, 0.3); opacity: 0; transform: translate3d(100%, 0px, 0px); transition: opacity 0.3s, transform 0s 0.3s;"></div>
          <div id="" class="bm-menu-wrap" style="position: fixed; right: inherit; z-index: 1100; width: 100vw; height: 100%; transform: translate3d(-100%, 0px, 0px); transition: all 0.5s;">
            <div class="bm-menu" style="height: 100%; box-sizing: border-box; overflow: auto;">
              <nav class="bm-item-list" style="height: 100%;">
                <div class="sidebar-container" style="background: white; width: 100vw; height: 100vh;">
                  <div class="sidebar-body" style="background: white; width: 100vw; height: 100vh;">
                    <ul>
                      <li>
                        <a>Đăng nhập</a>
                      </li>
                      <li>
                        <a href="https://www.mioto.vn/signup">Đăng kí</a>
                      </li>
                    </ul>
                  </div>
                  <div></div>
                </div>
              </nav>
            </div>
            <div>
              <div class="bm-cross-button" style="position: absolute; width: 24px; height: 24px; right: 8px; top: 8px;">
                <span style="position: absolute; top: 6px; right: 14px;">
                  <span class="bm-cross" style="position: absolute; width: 3px; height: 14px; transform: rotate(45deg);"></span>
                  <span class="bm-cross" style="position: absolute; width: 3px; height: 14px; transform: rotate(-45deg);"></span>
                </span>
                <button style="position: absolute; left: 0px; top: 0px; width: 100%; height: 100%; margin: 0px; padding: 0px; border: none; text-indent: -9999px; background: transparent; outline: none; cursor: pointer;">Close Menu</button>
              </div>
            </div>
          </div>
          <div>
            <div class="bm-burger-button" style="z-index: 1000;">
              <span>
                <span class="bm-burger-bars" style="position: absolute; height: 20%; left: 0px; right: 0px; top: 0%; opacity: 1;"></span>
                <span class="bm-burger-bars" style="position: absolute; height: 20%; left: 0px; right: 0px; top: 40%; opacity: 1;"></span>
                <span class="bm-burger-bars" style="position: absolute; height: 20%; left: 0px; right: 0px; top: 80%; opacity: 1;"></span>
              </span>
              <button style="position: absolute; left: 0px; top: 0px; width: 100%; height: 100%; margin: 0px; padding: 0px; border: none; opacity: 0; font-size: 8px; cursor: pointer;">Open Menu</button>
            </div>
          </div>
        </div>
        <section class="header">
          <div class="header-container">
            <div class="header-icon">
              <div class="js-toggle-right-slidebar js-menu-mobile">
                <div class="trigger-menu">
                  <span class="three-bars-icon"></span>
                </div>
              </div>
              <div>
                <div></div>
              </div>
              <a class="item-app show-on-med-and-down" href="https://www.mioto.vn/howitwork">
                <i class="ic ic-info-wh"></i>
              </a>
            </div>
            <div class="logo-container">
              <a href="https://www.mioto.vn/">Mioto</a>
            </div>
            <div class="info-container">
              <span>
                <i class="ic ic-phone"></i>
                <a href="tel:19009217" style="color: white;">1900 9217</a>
              </span>
              <span>
                <i class="ic ic-email"></i> contact@mioto.vn</span>
            </div>
            <div class="menu-container">
              <ul>
                <li class="has-magr">
                  <a href="https://www.mioto.vn/howitwork">
                    <i class="ic ic-info-wh"></i> Hướng dẫn</a>
                </li>
                <li>
                  <a>Đăng nhập</a>
                </li>
                <li>
                  <a class="btn btn-transparent btn--m" title="" href="https://www.mioto.vn/signup">Đăng kí</a>
                </li>
              </ul>
              <div></div>
            </div>
          </div>
        </section>
      </div>
  </section>
</template>
<script>
export default {};
</script>

