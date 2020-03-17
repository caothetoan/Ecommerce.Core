import axios from 'axios'
import app from '../main'; // import the instance

// const BASE_URL = 'http://api.ec.com/'
const BASE_URL = 'http://localhost:5000/'
const SSO_URL = 'http://id.ec.com/api/'

const instance = axios.create({
  baseURL: BASE_URL,
  // timeout: false,
  params: {} // do not remove this, its added to add params later in the config
})

// Add a request interceptor
instance.interceptors.request.use(function (config) {
  /* global window Store */
  const {token} = Store.state.user
  // console.log("token", token)
  config.headers.common['Access-Control-Allow-Origin'] = '*'
  config.headers.common['Content-Type'] = 'application/json'
  if (token) {
    config.headers.common['Authorization'] = 'Bearer ' + token
  } else {
    // Use application/x-www-form-urlencoded for login
    // config.headers.common['Content-Type'] = 'application/x-www-form-urlencoded'
  }
  app.$Progress.start(); // for every request start the progress
  return config
}, function (error) {
  // Do something with request error
  return Promise.reject(error)
})
instance.interceptors.response.use(response => {
  app.$Progress.finish(); // finish when a response is received
  return response;
});
instance.interceptors.response.use((response) => response, (error) => {
  console.log(error.config)
  return Promise.reject(error)
})

const handleErrors = err => {
  if (err && err.response && err.response.status === 400) {
    // console.log(err.response.data);
    // console.log(err.response.status);
    // console.log(err.response.headers);
  }
  return err.response;
};

const responseBody = res => res.data;

export default {
  getData (action) {
    let url = `${BASE_URL}`
    url += action
    return instance.get(url)
  },
  postData (action, data) {
    let url = `${BASE_URL}`
    url += action
    return instance.post(url, data)
  },
  putData (action, data) {
    let url = `${BASE_URL}`
    url += action
    return instance.put(url, data)
  },
  deleteData (action) {
    let url = `${BASE_URL}`
    url += action
    return instance.delete(url)
  },
  login (action, data) {
    let url = `${SSO_URL}`
    url += action
    return instance.post(url, data)
  },
  getToken (action) {
    let url = `${SSO_URL}`
    url += action
    return instance.get(url)
  }
}
