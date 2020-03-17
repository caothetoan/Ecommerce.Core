import Vue from 'vue'
import Router from 'vue-router'
import ErrorPage from '@/components/404'
import Home from '@/pages/Home'
import OrderList from '@/pages/OrderList'
import OrderForm from '@/pages/OrderForm'
import About from '@/pages/About'
import Articles from '@/pages/Articles'
import ArticleDetail from '@/pages/ArticleEdit'
import Find from '@/pages/Find'

import Login from '@/components/Login'
import ChangePassword from '@/components/ChangePassword'
import FileUpload from '@/components/FileUpload'

Vue.use(Router)

import auth from '@/utils/auth'

function requireAuth (to, from, next) {
  if (!auth.loggedIn()) {
    next({
      path: '/login',
      query: { redirect: to.fullPath }
    })
  } else {
    next()
  }
}

// const debug = process.env.NODE_ENV !== 'production'

export default new Router({
  base: __dirname,
  mode: 'history',
  // scrollBehavior: () => ({ y: 0 }),
  scrollBehavior: (to, from, savedPosition) => {
    if (to.hash) {
      return {
        selector: to.hash
      }
    }
    if (savedPosition) {
      return savedPosition
    } else {
      return { x: 0, y: 0 }
    }
  }, // routes are registered by theme or modules - here is only global router instance
  routes: [
    { path: '/404', component: ErrorPage, name: 'ErrorPage' },
    { path: '/about', component: About, name: 'About' },
    { path: '/find/filter/:address', component: Find, name: 'Find' },
    { path: '/orders', component: OrderList, name: 'Orders', beforeEnter: requireAuth },
    { path: '/order/:id', component: OrderForm, name: 'Order', beforeEnter: requireAuth },
    { path: '/article/:id', component: ArticleDetail, name: 'ArticleDetail' },
    { path: '/articles', component: Articles, name: 'Articles' },
    { path: '/FileUpload', component: FileUpload, name: 'FileUpload' },
    { path: '/login', component: Login, name: 'Login' },
    { path: '/changePassword', component: ChangePassword, name: 'ChangePassword', beforeEnter: requireAuth },
    { path: '/logout',
      beforeEnter (to, from, next) {
        auth.logout()
        next('/login')
      }
    },
    { path: '/', component: Home, name: 'Home' },
    { path: '*', redirect: '/404' }
  ],
  meta: {
    progress: {
      func: [
        {call: 'color', modifier: 'temp', argument: '#ffb000'},
        {call: 'fail', modifier: 'temp', argument: '#6e0000'},
        {call: 'location', modifier: 'temp', argument: 'top'},
        {call: 'transition', modifier: 'temp', argument: {speed: '1.5s', opacity: '0.6s', termination: 400}}
      ]
    }
  }
})
