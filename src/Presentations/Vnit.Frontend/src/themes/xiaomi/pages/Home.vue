<template>
      <section class="body">
        <div>

          <div class="home main">
            <div class="home-top">
              <Cover :items="items.featureCars" />
             
              <Promotion :items="items.featureCars" />

            </div>
            <div class="home-middle">
            
              <FeatureProducts :items="items.featureCars" />

              <FeatureNewsItem :items="items.topDest" />

            </div>
          </div>
         
          <Explorer />

        </div>
       
        <!--       
        <NewProducts :items="items.newCars" />-->
      </section>
</template>

<script>
/* globals Store */
import Cover from "@/components/Cover.vue";
import FeatureNewsItem from "@/components/FeatureNewsItem.vue";
import FeatureProducts from "@/components/FeatureProducts.vue";
import NewProducts from "@/components/NewProducts.vue";
import Promotion from "@/components/Promotion.vue";
import Explorer from "@/components/Explorer.vue";

import { mapState } from "vuex";

export default {
  components: {
    FeatureNewsItem,
    Cover,
    FeatureProducts,
    NewProducts,
    Promotion,
    Explorer
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
