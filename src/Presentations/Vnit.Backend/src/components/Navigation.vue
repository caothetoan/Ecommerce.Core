<template>
    <v-list>
        <v-list-tile v-for="item in items" :key="item.title" @click="clickMenu(item)" router>
        <v-list-tile-action class="pr-1 pl-2 mr-1">
            <v-icon :class="activeMenuItem.includes(item.title)?'blue--text': ''" :title="item.title" light v-html="item.icon"></v-icon>
        </v-list-tile-action>
        <v-list-tile-content :class="activeMenuItem.includes(item.title)?'blue--text': ''">
            <v-list-tile-title v-text="item.title"></v-list-tile-title>
        </v-list-tile-content>
        </v-list-tile>
    </v-list>
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
          title: "Categories",
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

