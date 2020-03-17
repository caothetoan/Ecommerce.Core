/* globals Store */
import api from "@/utils/backend-api";
import {Product} from "@/models";
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

const resourceUri = "servicepacks";

const state = {
  items: [],
  pagination: getDefaultPagination(),
  // page: 0,
  // pages: 0,
  loading: false,
  mode: "",
  snackbar: false,
  notice: "",
  product: new Product(),
  categories: [],
};

const getters = {
};

const actions = {
  getCategories ({ commit }) {
    api.getData("categories").then(res => {
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
  getProductById ({ commit }, id) {
    if (id) {
      api.getData(resourceUri + "/" + id + "?_expand=products").then(
        res => {
          const product = getResponseData(res);
          commit("setProduct", { product });
        },
        err => {
          console.log(err);
        }
      );
    } else {
      commit("setProduct", { product: new Product() });
    }
  },
  getItems ({ commit }, {query, pagination}) {
    commit("setLoading", { loading: true });
    let uri = resourceUri + "?page=" + pagination.page + "&count=" + pagination.rowsPerPage
    if (query) {
      uri +=  "&name=" + query
    }

    api.getData(uri).then(res => {
      const products = getResponseData(res);
      // products.forEach(p => {
      //   p.categoryName = p.category.categoryName;
      // });
      // commitPagination(commit, products);
      const totalItems = getResponseTotal(res);

      commitPagination(commit, products, pagination.page, totalItems, pagination.sortBy, pagination.descending);
      commit("setLoading", { loading: false });
    });
  },
  searchProducts ({ commit }, searchQuery) {
    api.getData(resourceUri + "?" + searchQuery).then(res => {
      const products = getResponseData(res);
      // products.forEach(p => {
      //   p.categoryName = p.category.categoryName;
      // });
      commitPagination(commit, products);
    });
  },
  quickSearch ({ commit }, { headers, qsFilter, pagination }) {
    // TODO: Following solution should be replaced by DB full-text search for production
    api.getData(resourceUri + "?name=" + qsFilter).then(res => {
      const products = getResponseData(res).filter(r =>
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
      products.forEach(p => {
        p.categoryName = p.category.categoryName;
      });
      commitPagination(commit, products);
    });
  },
  deleteProduct ({ commit, dispatch }, id) {
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
  saveProduct  ({ commit, dispatch }, product) {
    delete product.category;
    if (!product.id) {
      api
        .postData(resourceUri + "/", product)
        .then(res => {
          const product = getResponseData(res);
          commit("setProduct", { product });
          sendSuccessNotice(commit, "New product has been added.");
          closeNotice(commit, 1500);
        })
        .catch(err => {
          console.log(err);
          sendErrorNotice(commit, "Operation failed! Please try again later. ");
          closeNotice(commit, 1500);
        });
    } else {
      api
        .putData(resourceUri + "/" + product.id.toString(), product)
        .then(res => {
          const product = getResponseData(res);
          commit("setProduct", { product });
          sendSuccessNotice(commit, "Product has been updated.");
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
  setItems (state, products) {
    state.items = products;
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
  setProduct (state, {product}) {
    state.product = product
  }
};

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
};
