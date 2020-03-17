import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'
import user from './modules/user'
import products from './modules/products'
import orders from "./modules/orders";
import customers from "./modules/customers";
import articles from "./modules/articles";
import categories from "./modules/categories";
import newscategories from "./modules/newscategories";
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
    newscategories
  }
})