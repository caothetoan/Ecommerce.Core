import moment from 'moment'
import Vue from 'vue'
Vue.filter('formatDate', function (value, format) {
  if (value) {
    if (!format) {
      format = 'DD/MM/YYYY HH:mm:ss';
    }
    return moment(String(value)).format(format)
  }
});
