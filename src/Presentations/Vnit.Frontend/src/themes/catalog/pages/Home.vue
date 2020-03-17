<template>
      <section class="body">
     
        <Cover :items="'https://www.mioto.vn/static/media/banner_home.94aa1b72.jpg' || appinfo.bg[0]" />

        <TopDestination :items="items.topDest" />

        <Promotion :items="'https://www.mioto.vn/static/media/banner-7.e9315f36.png' || appinfo.bg[1]" />

        <FeatureCars :items="items.featureCars" />

        <Explorer :items="'https://www.mioto.vn/static/media/bg-explore.de0dbafe.jpg' || appinfo.bg[2]" />

        <NewCars :items="items.newCars" />

        <DownloadApp />
      </section>
</template>

<script>
/* globals Store */
import Cover from "@/components/Cover.vue";
import TopDestination from "@/components/TopDestination.vue";
import FeatureCars from "@/components/FeatureCars.vue";
import NewCars from "@/components/NewCars.vue";
import Promotion from "@/components/Promotion.vue";
import Explorer from "@/components/Explorer.vue";
import DownloadApp from "@/components/DownloadApp.vue";

import { mapState } from "vuex";

export default {
  components: {
    TopDestination,
    Cover,
    FeatureCars,
    NewCars,
    Promotion,
    Explorer,
    DownloadApp
  },
  methods: {
    search() {

    },
    getAppinfo() {
      Store.dispatch("appinfo/getItems");
    },
    getHomepage() {
      Store.dispatch("homepage/getItems");
    }
  },
  computed: {
    ...mapState("homepage", {
      items: "items",
      pagination: "pagination",
      loading: "loading",
      mode: "mode",
      snackbar: "snackbar",
      notice: "notice"
    }),
    ...mapState("appinfo", {
      appinfo: "appinfo"
    }),
  },
  created() {
    this.getHomepage();
    this.getAppinfo();
  },
  mounted() {
  }
};
</script>
