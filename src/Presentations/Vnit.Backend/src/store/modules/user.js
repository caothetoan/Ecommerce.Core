import Vue from "vue"
import Vuex from "vuex"
import api from "@/utils/backend-api";
Vue.use(Vuex)
import {
  sendSuccessNotice,
  sendErrorNotice,
  closeNotice,
  getDefaultPagination,
  commitPagination,
  getResponseData,
  getResponseTotal
} from "@/utils/store-util.js";
const resourceUri = "users";
const state = {

  callingAPI: false,
  searching: "",
  user: null,
  token: null,
  userInfo: {
    messages: [],
    notifications: [],
    tasks: []
  }
}

const actions = {
  getUser ({ commit }) {
    api.getData(resourceUri + '/current').then(res => {
      if (res.data) {
        const user = getResponseData(res)
        commit("setUser", user)
      }
    });
  },
  quickSearch ({ commit }, { headers, qsFilter, pagination }) {
    // TODO: Following solution should be replaced by DB full-text search for production
    api.getData(resourceUri + "?name=" + qsFilter).then(res => {
      const users = getResponseData(res)
      commitPagination(commit, users);
    });
  },
  setToken ({ commit }, { token }) {
    commit("setToken", token)
  },
  updateUser ({ commit }, { user, token }) {
    commit("setToken", token)
    if (user) {
      commit("setUser", user)
    }
  },
  logout ({ commit }) {
    commit("setToken", null)
    commit("setUser", {})
  }
}

const mutations = {
  loginLoading (state) {
    state.callingAPI = !state.callingAPI
  },
  globalSearching (state) {
    state.searching = state.searching === "" ? "loading" : ""
  },
  setUser (state, user) {
    state.user = user
  },
  setToken (state, token) {
    state.token = token
  },
  setUserInfo (state, userInfo) {
    state.userInfo = userInfo
  }
}

export default {
  namespaced: true,
  state,
  actions,
  mutations
}
