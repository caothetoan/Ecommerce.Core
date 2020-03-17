import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'
import user from './modules/user'
import products from './modules/products'
import orders from "./modules/orders";
import customers from "./modules/customers";
import articles from "./modules/articles";
import categories from "./modules/categories";
import homepage from "./modules/homepage";
import appinfo from "./modules/appinfo";
import car from "./modules/car";
Vue.use(Vuex)


export default new Vuex.Store({
  plugins: [createPersistedState({ storage: window.sessionStorage })], // !debug ? [createPersistedState({ storage: window.sessionStorage })] : [],
  modules: {
    user,
    products,
    orders,
    customers,
    articles,
    categories,
    homepage,
    appinfo,
    car
  }
})
