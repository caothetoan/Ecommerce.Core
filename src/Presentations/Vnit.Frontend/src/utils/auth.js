/* globals Store */
import api from './backend-api'

export default {
  login (email, pass, cb) {
    cb = arguments[arguments.length - 1]
    const clientId = '3a56fe2f-d707-4606-9aaa-9ba3135f7b04'
    let data = 'grant_type=password&username=' + email + '&password=' + pass + '&client_id=' + clientId
    api.login('oauth2/token', data).then((res) => {
      const token = res.access_token || res.data.access_token
      const user = res.user || res.data.user
      // Store.dispatch('user/updateUser', {user, token})
      Store.dispatch('user/setToken', {token})
      Store.dispatch('user/getUser')
      if (cb) cb(true, null)
      this.onChange(true)
    }, (err) => {
      console.log(err)
      if (cb) cb(false, err)
      this.onChange(false)
    })
  },
  getToken () {
    return Store.state.user.token
  },
  logout (cb) {
    // delete localStorage.token
    // Store.commit('setToken', null)
    Store.dispatch('user/logout')
    if (cb) cb(false)
    this.onChange(false)
  },
  loggedIn () {
    return !!Store.state.user.token
  },
  onChange () {}
}
