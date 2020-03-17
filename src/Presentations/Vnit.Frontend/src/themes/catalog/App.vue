<template>
  <div class="">
    <vue-progress-bar>
    </vue-progress-bar>
    <Header />
    <template v-if="!loggedIn">
      <router-view></router-view>
    </template>
    <template v-if="loggedIn">
      <router-view></router-view>
    </template>
    <Footer />
  </div>
</template>
<script>

  import Header from "@/components/Header.vue";
  import Footer from "@/components/Footer.vue";
  import auth from "./utils/auth";
  import { mapState } from "vuex";
  export default {
    components: {
    Header,
    Footer
  },
    data () {
      return {
        dialog: false,
        mini: false,
        dialogText: "",
        dialogTitle: "",
        loggedIn: auth.loggedIn(),
        isRootComponent: true,
        // clipped: false,
        drawer: true,
        fixed: false,
        userMenus: [
          {
            icon: "bubble_chart",
            title: "Logout",
            link: "/logout"
          },
          {
            icon: "bubble_chart",
            title: "Change Password",
            link: "/changepassword"
          }
        ],
        miniVariant: false,
        right: true,
        rightDrawer: false,
        menuItem: "Orders"
      };
    },
    created () {
      auth.onChange = loggedIn => {
        console.log("loggedIn", loggedIn);
        this.loggedIn = loggedIn;
      };
      //  [App.vue specific] When App.vue is first loaded start the progress bar
      this.$Progress.start();
      //  hook the progress bar to start before we move router-view
      this.$router.beforeEach((to, from, next) => {
        //  does the page we want to go to have a meta.progress object
        if (to.meta.progress !== undefined) {
          let meta = to.meta.progress;
          // parse meta tags
          this.$Progress.parseMeta(meta);
        }
        //  start the progress bar
        this.$Progress.start();
        //  continue to next page
        next();
      });
      //  hook the progress bar to finish after we've finished moving router-view
      this.$router.afterEach((to, from) => {
        if (to.name !== "ErrorPage") {
          this.menuItem = to.name;
        }
        //  finish the progress bar
        this.$Progress.finish();
      });
    },
    computed: {
      ...mapState("user", {
        user: "user"
      }),
      auth () {
        return auth;
      },
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
      },
    },
    mounted () {
      this.$Progress.finish();
    }
  };
</script>
<style>
  @import './assets/css/style.css';
</style>
