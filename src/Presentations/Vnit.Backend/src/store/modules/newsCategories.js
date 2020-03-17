/* globals Store */
import api from "@/utils/backend-api";
import {NewsCategory} from "@/models";
import {
  sendSuccessNotice,
  sendErrorNotice,
  closeNotice,
  getDefaultPagination,
  commitPagination,
  getResponseData,
  getResponseTotal
} from "@/utils/store-util.js";
import {get} from "lodash"

const resourceUri = "newscategorys";
const state = {
  category: new NewsCategory(),
  categories: [],
  categoriesAll: [],
  pagination: getDefaultPagination(),
  // page: 0,
  // pages: 0,
  loading: false,
  mode: "",
  snackbar: false,
  notice: ""
};

const getters = {
};

const actions = {
  getCategories ({ commit }, {query, pagination}) {
    // commit("setLoading", { loading: true });
    let uri = resourceUri + "?page=" + pagination.page + "&count=" + pagination.rowsPerPage
    if (query) {
      uri +=  "&name=" + query
    }
    api.getData(uri).then(res => {
      const categories = getResponseData(res);
      const totalItems = getResponseTotal(res);
      commitPagination(commit, categories, pagination.page, totalItems, pagination.sortBy, pagination.descending);
      // commit("setLoading", { loading: false });
    });
  },
  getCategoryById ({ commit }, id) {
    if (id) {
      api.getData(resourceUri + "/" + id + "").then(
        res => {
          const category = getResponseData(res);
          commit("setCategory", { category });
        },
        err => {
          console.log(err);
        }
      );
    } else {
      commit("setCategory", { category: new NewsCategory() });
    }
  },
  quickSearch ({ commit }, { headers, query, pagination }) {
    let uri = resourceUri + "?page=" + pagination.page + "&count=" + pagination.rowsPerPage
    if (query) {
      uri +=  "&name=" + query
    }
    api.getData(uri).then(res => {
      const categories = getResponseData(res).filter(r =>
        headers.some(header => {
          const val = get(r, [header.value]);
          return (
            (val &&
              val
                .toString()
                .toLowerCase()
                .includes(query)) ||
            false
          );
        })
      );
      const totalItems = getResponseTotal(res);
      commitPagination(commit, categories, pagination.page, totalItems, pagination.sortBy, pagination.descending);
    });
  },
  delete ({ commit, dispatch }, id) {
    api
      .deleteData(resourceUri + "/" + id.toString())
      .then(res => {
        return new Promise((resolve, reject) => {
          sendSuccessNotice(commit, "Operation is done.");
          resolve();
        });
      })
      .catch(err => {
        console.log(err);
        sendErrorNotice(commit, "Operation failed! Please try again later. ");
        closeNotice(commit, 1500);
      });
  },
  save ({ commit, dispatch }, category) {
    if (!category.id) {
      api
        .postData(resourceUri, category)
        .then(res => {
          const category = getResponseData(res);
          commit("setCategory", { category });
          sendSuccessNotice(commit, res.data.messages);
          closeNotice(commit, 1500);
        })
        .catch(err => {
          console.log(err);
          sendErrorNotice(commit, "Operation failed! Please try again later. ");
          closeNotice(commit, 1500);
        });
    } else {
      api
        .putData(resourceUri + "/" + category.id.toString(), category)
        .then(res => {
          const category = getResponseData(res);
          commit("setCategory", { category });
          sendSuccessNotice(commit, res.data.messages);
          closeNotice(commit, 1500);
        })
        .catch(err => {
          console.log(err);
          sendErrorNotice(commit, "Operation failed! Please try again later. ");
          closeNotice(commit, 1500);
        });
    }
  },
  closeSnackBar ({ commit }, timeout) {
    closeNotice(commit, timeout);
  },
};

const mutations = {
  setItems (state, categories) {
    state.categories = categories;
  },
  setAllCategories (state, categories) {
    state.categoriesAll = categories;
  },
  setPagination (state, pagination) {
    state.pagination = pagination;
  },
  setLoading (state, { loading }) {
    state.loading = loading;
  },
  setNotice (state, { notice }) {
    console.log(" notice .... ", notice);
    state.notice = notice;
  },
  setSnackbar (state, { snackbar }) {
    state.snackbar = snackbar;
  },
  setMode (state, { mode }) {
    state.mode = mode;
  },
  setCategory (state, {category}) {
    state.category = category
  }
};

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
};
