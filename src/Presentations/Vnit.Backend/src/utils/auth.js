/* globals Store */
import api from './backend-api'

export default {
  login (email, password, cb) {
    cb = arguments[arguments.length - 1]
    const clientId = '3a56fe2f-d707-4606-9aaa-9ba3135f7b04'
    let data = {
      email: email,
      password: password,
      client_id: clientId,
      grant_type: password,
      returnUrl: '',
      requiresTwoFactor: false,
      TwoFactorCode: false,
      RememberMachine: true,
      RememberMe: true
    }

    api.login('accounts/signin', data).then((res) => {
      const token = res.data.responseData.token
      const user = res.data.responseData.user
      if (token) {
        Store.dispatch('user/updateUser', {user, token})
        // Store.dispatch('user/setToken', {token})
        // Store.dispatch('user/getUser')
        if (cb) cb(true, null)
        this.onChange(true)
      } else {
        console.log(res)
        if (cb) cb(false, res)
        this.onChange(false)
      }
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
    Store.dispatch('user/logout')
    if (cb) cb(false)
    this.onChange(false)
  },
  loggedIn () {
    return !!Store.state.user.token
  },
  onChange () {}
}
