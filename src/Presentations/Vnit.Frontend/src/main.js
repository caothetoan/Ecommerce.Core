// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import Vuetify from 'vuetify'
import Meta from 'vue-meta'
import VueLazyload from 'vue-lazyload'
import i18n from './lang/i18n'
import VueOffline from 'vue-offline'

import App from './themes/default/App'
import router from './themes/default/router'
import store from '@/store'
import api from './utils/backend-api'
import appUtil from './utils/app-util'

import VueProgressBar from 'vue-progressbar'
const options = {
  color: '#2196f3',
  failedColor: '#874b4b',
  thickness: '5px',
  transition: {
    speed: '0.1s',
    opacity: '0.5s',
    termination: 400
  },
  autoRevert: true,
  location: 'top',
  inverse: false
}

Vue.use(VueProgressBar, options)

Vue.use(Vuetify)
Vue.use(VueLazyload, { attempt: 2 })
Vue.use(Meta)
Vue.use(VueOffline)

Vue.config.productionTip = false

window.Store = store
Vue.prototype.api = api
Vue.prototype.appUtil = appUtil

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  i18n,
  render: h => h(App)
  // template: '<App/>',
})
