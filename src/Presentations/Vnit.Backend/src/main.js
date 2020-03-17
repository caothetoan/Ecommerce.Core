// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import Vuetify from 'vuetify'

Vue.use(Vuetify)

// Vuelidate
import Vuelidate from 'vuelidate'
Vue.use(Vuelidate)

import progressBar from '@/plugins/progressBar'
import Meta from 'vue-meta'
import VueTinymce from 'vue-tinymce'
Vue.use(VueTinymce)

import App from './App'
import router from '@/router'
import store from '@/store'
import api from './utils/backend-api'
import appUtil from './utils/app-util'


Vue.config.productionTip = false

window.Store = store
Vue.prototype.api = api
Vue.prototype.appUtil = appUtil

/* eslint-disable no-new */
export default new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
  // template: '<App/>',
})
