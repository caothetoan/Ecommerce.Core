<template>
    <div>


      <div class="search-menu orange">
        <div class="container">
          <form class="mainsearch" onsubmit="return submitSearch(this)">
            <div class="pr">
              <input type="search" name="key" placeholder="Bạn mua gì?" maxlength="50" onkeyup="SuggestSearch(this,-1)" onkeydown="clearDelaysuggest()" autocomplete="off">
                <button type="submit" class="btnsearch"><i class="icon-search"></i></button> <span id="searchclear" class="searchclear"><i class="icon-searchclr"></i></span>
            </div>
          </form>
          <div class="hotline"><img src="//cdn.tgdd.vn/vuivui/www/Content/images/desktop/delivery-motorbike.png"><a href="/tuyen-dung" target="_self"> Cần 500 anh em Giao hàng xe máy. Ứng tuyển ngay TẠI ĐÂY »</a></div>
          <div class="clr"></div>
        </div>
      </div>
    </div>
</template>
<script>
export default {
  data() {
    return {
      menuItem: "Dashboard",
      items: [
        {
          icon: "dashboard",
          title: "Dashboard",
          vertical: "Dashboard",
          link: "dashboard"
        },
        {
          icon: "shopping_cart",
          title: "Orders",
          vertical: "Order",
          link: "orders"
        },
        {
          icon: "perm_identity",
          title: "Customers",
          vertical: "Customer",
          link: "customers"
        },
        {
          icon: "bubble_chart",
          title: "Products",
          vertical: "Product",
          link: "products"
        },
        {
          icon: "category",
          title: "Category",
          vertical: "categories",
          link: "categories"
        },
        {
          icon: "book",
          title: "Articles",
          vertical: "Articles",
          link: "articles"
        },
        {
          icon: "add",
          title: "Editor",
          vertical: "Editor",
          link: "editor"
        },

        {
          icon: "thumbs_up_down",
          title: "About",
          vertical: "About",
          link: "about"
        }
      ]
    };
  },
  created() {
    //  hook the progress bar to finish after we've finished moving router-view
    this.$router.afterEach((to, from) => {
      if (to.name !== "ErrorPage") {
        this.menuItem = to.name;
      }
    });
  },
  computed: {
    activeMenuItem() {
      return this.menuItem;
    }
  },
  methods: {
    clickMenu(item) {
      this.menuItem = item.title;
      this.$router.push({
        name: item.title
      });
    }
  }
};
</script>

