/* globals Store */
import api from "@/utils/backend-api";
import {Article} from "@/models";
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

const resourceUri = "newsItems";
const state = {
  items: [],
  pagination: getDefaultPagination(),
  // page: 0,
  // pages: 0,
  loading: false,
  mode: "",
  snackbar: false,
  notice: "",
  article: new Article(),
  categories: [],
};

const getters = {
};

const actions = {
  getCategories ({ commit }) {
    api.getData("NewsCategorys").then(res => {
      const categories = [];
      getResponseData(res).forEach(c => {
        const category = { ...c };
        category.text = c.categoryName;
        category.value = c.id;
        categories.push(category);
      });
      commit("setCategories", categories);
    });
  },
  getArticleById ({ commit }, id) {
    if (id) {
      api.getData(resourceUri + id).then(
        res => {
          const article = getResponseData(res);
          commit("setArticle", { article });
        },
        err => {
          console.log(err);
        }
      );
    } else {
      commit("setArticle", { article: new Article() });
    }
  },
  // getAll ({ commit }) {
  //   commit("setLoading", { loading: true });
  //   api.getData(resourceUri + "?_expand=articles").then(res => {
  //     const articles = getResponseData(res);
  //     const totalItems = getResponseTotal(res);
  //     // articles.forEach(p => {
  //     //   p.categoryName = p.category.categoryName;
  //     // });
  //     commitPagination(commit, articles, totalItems);
  //     commit("setLoading", { loading: false });
  //   });
  // },
  getItems ({ commit }, {query, pagination}) {
    commit("setLoading", { loading: true });
    let uri = resourceUri + "?page=" + pagination.page + "&count=" + pagination.rowsPerPage
    if (query) {
      uri +=  "&name=" + query
    }
    api.getData(uri).then(res => {
      const articles = getResponseData(res);
      const totalItems = getResponseTotal(res);
      commitPagination(commit, articles, pagination.page, totalItems, pagination.sortBy, pagination.descending);
      commit("setLoading", { loading: false });
    });
  },
  search ({ commit }, {query, pagination}) {
    api.getData(resourceUri + "?name=" + query + "&page=" + pagination.page + "&count=" + pagination.rowsPerPage).then(res => {
      const articles = getResponseData(res);
      // articles.forEach(p => {
      //   p.categoryName = p.category.categoryName;
      // });
      const totalItems = getResponseTotal(res);
      commitPagination(commit, articles, 1, totalItems, pagination.sortBy, pagination.descending);
    });
  },
  quickSearch ({ commit }, { headers, qsFilter, pagination }) {
    api.getData(resourceUri + "?name=" + qsFilter + "&page=" + pagination.page + "&count=" + pagination.rowsPerPage).then(res => {
      const articles = getResponseData(res);
      // articles.forEach(p => {
      //   p.categoryName = p.category.categoryName;
      // });
      const totalItems = getResponseTotal(res);
      commitPagination(commit, articles, 1, totalItems, pagination.sortBy, pagination.descending);
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
  save ({ commit, dispatch }, article) {
    delete article.category;
    if (!article.id) {
      api
        .postData(resourceUri + "/", article)
        .then(res => {
          const article = getResponseData(res);
          commit("setArticle", { article });
          sendSuccessNotice(commit, "New article has been added.");
          closeNotice(commit, 1500);
        })
        .catch(err => {
          console.log(err);
          sendErrorNotice(commit, "Operation failed! Please try again later. ");
          closeNotice(commit, 1500);
        });
    } else {
      api
        .putData(resourceUri + "/" + article.id.toString(), article)
        .then(res => {
          const article = getResponseData(res);
          commit("setArticle", { article });
          sendSuccessNotice(commit, "Article has been updated.");
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
  setCategories(state, newscategories) {
    state.newscategories = newscategories;
  },
  setItems (state, articles) {
    state.items = articles;
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
  setArticle (state, {article}) {
    state.article = article
  }
};

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
};
