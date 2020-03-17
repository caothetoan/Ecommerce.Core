/* globals Store */
import api from "@/utils/backend-api";
import {Category} from "@/models";
import {
  sendSuccessNotice,
  sendErrorNotice,
  closeNotice,
  getDefaultPagination,
  commitPagination,
  getResponseData
} from "@/utils/store-util.js";
import {get} from "lodash"

const resourceUri = "newscategories";
const state = {
  items: [],
  pagination: getDefaultPagination(),
  // page: 0,
  // pages: 0,
  loading: false,
  mode: "",
  snackbar: false,
  notice: "",
  category: new Category(),
  categories: [],
};

const getters = {
};

const actions = {
  getCategories ({ commit }) {
    api.getData(resourceUri).then(res => {
      const categories = [];
      res.data.forEach(c => {
        const category = { ...c };
        category.text = c.categoryName;
        category.value = c.id;
        categories.push(category);
      });
      commit("setCategories", categories);
    });
  },
  getCategoryById ({ commit }, id) {
    if (id) {
      api.getData(resourceUri + "/" + id + "?_expand=category").then(
        res => {
          const category = getResponseData(res);
          commit("setCategory", { category });
        },
        err => {
          console.log(err);
        }
      );
    } else {
      commit("setCategory", { category: new Category() });
    }
  },
  getAll ({ commit }) {
    commit("setLoading", { loading: true });
    api.getData(resourceUri + "?page=1&count=100&_expand=category").then(res => {
      const categories = getResponseData(res);

      commitPagination(commit, categories);
      commit("setLoading", { loading: false });
    });
  },
  search ({ commit }, searchQuery) {
    api.getData(resourceUri + "?_expand=category&" + searchQuery).then(res => {
      const categories = getResponseData(res);

      commitPagination(commit, categories);
    });
  },
  quickSearch ({ commit }, { headers, qsFilter, pagination }) {
    // TODO: Following solution should be replaced by DB full-text search for production
    api.getData(resourceUri + "?name=" + qsFilter).then(res => {
      const categories = getResponseData(res).filter(r =>
        headers.some(header => {
          const val = get(r, [header.value]);
          return (
            (val &&
              val
                .toString()
                .toLowerCase()
                .includes(qsFilter)) ||
            false
          );
        })
      );

      commitPagination(commit, categories);
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
        .postData(resourceUri + "/", category)
        .then(res => {
          const category = getResponseData(res);
          commit("setCategory", { category });
          sendSuccessNotice(commit, "New category has been added.");
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
          sendSuccessNotice(commit, "category has been updated.");
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
  setCategories (state, categories) {
    state.categories = categories;
  },
  setItems (state, categories) {
    state.items = categories;
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
