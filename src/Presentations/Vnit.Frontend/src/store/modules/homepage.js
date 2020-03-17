/* globals Store */
import api from "@/utils/backend-api";
import {
  sendSuccessNotice,
  sendErrorNotice,
  closeNotice
} from "@/utils/store-util.js";
const resourceUri = "homepage";

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
      const homepage = data.data
      commit("setItems", homepage);
    });
  }
};

const mutations = {
  setItems (state, homepage) {
    state.items = homepage;
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
