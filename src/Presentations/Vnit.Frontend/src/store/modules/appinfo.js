/* globals Store */
import api from "@/utils/backend-api";
const resourceUri = "init/app";

const state = {
  items: [],
  loading: false,
  mode: "",
  snackbar: false,
  notice: ""
};

const getters = {
};

const actions = {
  getItems ({ commit }) {
    api.getData(resourceUri).then(res => {
      const data = res.data
      if (!data) {
        console.log('response data is null')
      }
      const appinfo = data.data
      commit("setItems", appinfo);
    });
  }
};

const mutations = {
  setItems (state, homepage) {
    state.appinfo = homepage;
  },

  setLoading (state, { loading }) {
    state.loading = loading;
  }
};

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
};
