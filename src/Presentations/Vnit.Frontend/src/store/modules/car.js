/* globals Store */
import api from "@/utils/backend-api";
import {
  sendSuccessNotice,
  sendErrorNotice,
  closeNotice,
  getDefaultPagination,
  commitPagination,
  getResponseData,
  getResponseTotal
} from "@/utils/store-util.js";

const state = {
  items: [],
  pagination: getDefaultPagination(),
  loading: false,
  mode: "",
  snackbar: false,
  notice: "",
  categories: [],
};

const getters = {
};

const actions = {
  // v2/car/search/map?lat=21.0309274&lon=105.78403730000002&st=1532440800000&et=1532523600000&sort=0&radius=1.6937713499393958&radiusB=5.081314049818188&w=3.3875426998787916&h=3.3875426998787916&uw=0.42344283748484896&uh=0.42344283748484896
  searchCars ({ commit }, {query, pagination}) {
    commit("setLoading", { loading: true });
    let uri = "car/search/list?lat=21.0309274&lon=105.78403730000002&st=1532440800000&et=1532523600000&sort=0&pos=0&page=" + pagination.page + "&count=" + pagination.rowsPerPage
    if (query) {
      uri +=  "&name=" + query
    }
    api.getData(uri).then(res => {
      const cars = getResponseData(res);
      const totalItems = getResponseTotal(res);
      commitPagination(commit, cars, pagination.page, totalItems, pagination.sortBy, pagination.descending);
      commit("setLoading", { loading: false });
    });
  },
  getVehicleMakes ({ commit }) {
    api.getData("vehicle/makes").then(res => {
      const vehicleMakes = getResponseData(res);
      commit("setVehicleMakes", vehicleMakes);
    });
  },
  getVehicleTypes ({ commit }) {
    api.getData("vehicle/types").then(res => {
      const vehicleTypes = getResponseData(res);
      commit("setVehicleTypes", { vehicleTypes });
    });
  },
  getById ({ commit }, id) {
    if (id) {
      api.getData("car/search/detail?id=" + id).then(
        res => {
          const car = getResponseData(res);
          commit("setCar", { car });
        },
        err => {
          console.log(err);
        }
      );
    }
  },
  filterCars ({ commit }, {query, pagination}) {
    api.getData("car/filter?address=" + query + "&page=" + pagination.page + "&count=" + pagination.rowsPerPage).then(res => {
      const filterCars = getResponseData(res);
      commit("setFilterCars", { filterCars });
    });
  },
  closeSnackBar ({ commit }, timeout) {
    closeNotice(commit, timeout);
  },
};

const mutations = {
  setVehicleTypes (state, vehicleTypes) {
    state.vehicleTypes = vehicleTypes;
  },
  setVehicleMakes (state, vehicleMakes) {
    state.vehicleMakes = vehicleMakes;
  },
  setFilterCars (state, filterCars) {
    state.filterCars = filterCars;
  },
  setItems (state, cars) {
    state.cars = cars;
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
  setCar (state, {car}) {
    state.car = car
  }
};

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
};
