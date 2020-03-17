<template>
  <v-flex xs12 sm10 offset-sm1>
    <div class="display-1 my-3">Date picker</div>
    <div class="mb-2">
      <v-layout row wrap>
        <v-flex xs12 sm3>
          <v-checkbox v-model="landscape" hide-details label="Landscape"></v-checkbox>
        </v-flex>
        <v-flex xs12 sm3>
          <v-checkbox v-model="reactive" hide-details label="Reactive"></v-checkbox>
        </v-flex>
      </v-layout>
    </div>
    <v-date-picker v-model="picker" :landscape="landscape" :reactive="reactive"></v-date-picker>
    <div class="title py-3 mt-5">Date picker Colors</div>
    <v-layout row wrap>
      <v-flex xs12 sm6>
        <v-date-picker v-model="picker2" color="green lighten-1"></v-date-picker>
      </v-flex>
      <v-flex xs12 sm6 class="hidden-xs-only">
        <v-date-picker v-model="picker3" color="green lighten-1" header-color="primary"></v-date-picker>
      </v-flex>
    </v-layout>
    <div class="title py-3 mt-5">Date picker - In dialog and menu</div>
    <v-layout row wrap>
      <v-flex xs12 sm6 md4>
        <v-menu
          ref="menu"
          :close-on-content-click="false"
          v-model="menu"
          :nudge-right="40"
          :return-value.sync="date"
          lazy
          transition="scale-transition"
          offset-y
          full-width
          min-width="290px"
        >
          <v-text-field
            slot="activator"
            v-model="date"
            label="Picker in menu"
            prepend-icon="event"
            readonly
          ></v-text-field>
          <v-date-picker v-model="date" no-title scrollable>
            <v-spacer></v-spacer>
            <v-btn flat color="primary" @click="menu = false">Cancel</v-btn>
            <v-btn flat color="primary" @click="$refs.menu.save(date)">OK</v-btn>
          </v-date-picker>
        </v-menu>
      </v-flex>
      <v-spacer></v-spacer>
      <v-flex xs12 sm6 md4>
        <v-dialog
          ref="dialog"
          v-model="modal"
          :return-value.sync="date"
          persistent
          lazy
          full-width
          width="290px"
        >
          <v-text-field
            slot="activator"
            v-model="date"
            label="Picker in dialog"
            prepend-icon="event"
            readonly
          ></v-text-field>
          <v-date-picker v-model="date" scrollable>
            <v-spacer></v-spacer>
            <v-btn flat color="primary" @click="modal = false">Cancel</v-btn>
            <v-btn flat color="primary" @click="$refs.dialog.save(date)">OK</v-btn>
          </v-date-picker>
        </v-dialog>
      </v-flex>
      <v-flex xs12 sm6 md4>
        <v-menu
          ref="menu2"
          :close-on-content-click="false"
          v-model="menu2"
          :nudge-right="40"
          :return-value.sync="date"
          lazy
          transition="scale-transition"
          offset-y
          full-width
          min-width="290px"
        >
          <v-text-field
            slot="activator"
            v-model="date"
            label="Picker without buttons"
            prepend-icon="event"
            readonly
          ></v-text-field>
          <v-date-picker v-model="date" @input="$refs.menu2.save(date)"></v-date-picker>

        </v-menu>
      </v-flex>
      <v-spacer></v-spacer>
    </v-layout>
    <div class="title py-3 mt-5">Date picker Allowed dates</div>
    <v-date-picker
      v-model="date1"
      :allowed-dates="allowedDates"
      class="mt-3"
      min="2016-06-15"
      max="2018-03-20"
    ></v-date-picker>
    <div class="title py-3 mt-5">Birthday picker</div>
    <v-menu
      ref="menu5"
      :close-on-content-click="false"
      v-model="menu5"
      :nudge-right="40"
      lazy
      transition="scale-transition"
      offset-y
      full-width
      min-width="290px"
    >
      <v-text-field
        slot="activator"
        v-model="date5"
        label="Birthday date"
        prepend-icon="event"
        readonly
      ></v-text-field>
      <v-date-picker
        ref="picker5"
        v-model="date5"
        :max="new Date().toISOString().substr(0, 10)"
        min="1950-01-01"
        @change="save"
      ></v-date-picker>
    </v-menu>
  </v-flex>
</template>
<script>
  export default {
    name: 'DatePickers',
    data () {
      return {
        picker: null,
        landscape: false,
        reactive: false,
        picker2: null,
        picker3: null,
        date: null,
        menu: false,
        modal: false,
        menu2: false,
        date1: '2018-03-02',
        date5: null,
        menu5: false
      }
    },

    methods: {
      allowedDates: val => parseInt(val.split('-')[2], 10) % 2 === 0,
      save (date) {
        this.$refs.menu5.save(date)
      }
    },
    watch: {
      menu5 (val) {
        val && this.$nextTick(() => (this.$refs.picker5.activePicker = 'YEAR'))
      }
    },
    created: function(){
      this.$root.breadcrumbs = [
        {
          text: 'Dashboard',
          disabled: false,
          href: '/dashboard'
        },
        {
          text: 'Pickers',
          disabled: true
        },
        {
          text: 'Date Pickers',
          disabled: true
        }
      ]
    }
  }
</script>