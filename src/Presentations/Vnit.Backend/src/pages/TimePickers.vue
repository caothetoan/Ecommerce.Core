<template>
  <v-flex xs12 sm10 offset-sm1>
    <div class="display-1 my-3">Time picker</div>
    <v-checkbox v-model="landscape" label="Landscape"></v-checkbox>
    <v-time-picker v-model="picker" :landscape="landscape"></v-time-picker>
    <div class="title py-3 mt-5">Time picker Colors</div>
    <v-layout row wrap>
      <v-flex md12 lg6>
        <v-time-picker v-model="e4" color="green lighten-1"></v-time-picker>
      </v-flex>
      <v-flex md12 lg6 class="hidden-xs-only">
        <v-time-picker v-model="e4" color="green lighten-1" header-color="primary"></v-time-picker>
      </v-flex>
    </v-layout>
    <div class="title py-3 mt-5">Time picker - In dialog and menu</div>
    <v-layout row wrap>
      <v-flex xs11 sm5>
        <v-menu
          ref="menu"
          :close-on-content-click="false"
          v-model="menu2"
          :nudge-right="40"
          :return-value.sync="time"
          lazy
          transition="scale-transition"
          offset-y
          full-width
          max-width="290px"
          min-width="290px"
        >
          <v-text-field
            slot="activator"
            v-model="time"
            label="Picker in menu"
            prepend-icon="access_time"
            readonly
          ></v-text-field>
          <v-time-picker v-model="time" @change="$refs.menu.save(time)"></v-time-picker>
        </v-menu>
      </v-flex>
      <v-spacer></v-spacer>
      <v-flex xs11 sm5>
        <v-dialog
          ref="dialog"
          v-model="modal2"
          :return-value.sync="time"
          persistent
          lazy
          full-width
          width="290px"
        >
          <v-text-field
            slot="activator"
            v-model="time"
            label="Picker in dialog"
            prepend-icon="access_time"
            readonly
          ></v-text-field>
          <v-time-picker v-model="time" actions>
            <v-spacer></v-spacer>
            <v-btn flat color="primary" @click="modal2 = false">Cancel</v-btn>
            <v-btn flat color="primary" @click="$refs.dialog.save(time)">OK</v-btn>
          </v-time-picker>
        </v-dialog>
      </v-flex>
    </v-layout>
    <div class="title py-3 mt-5">Time picker - 24h format</div>
    <v-layout row wrap>
      <v-flex md12 lg4 lg-offset8>
        <v-time-picker v-model="e7" format="24hr"></v-time-picker>
      </v-flex>
    </v-layout>
    <div class="title py-3 mt-5">Time picker Allowed times</div>
    <v-time-picker
      v-model="time2"
      :allowed-hours="allowedHours"
      :allowed-minutes="allowedMinutes"
      class="mt-3"
      format="24hr"
      scrollable
      min="9:30"
      max="22:15"
    ></v-time-picker>
  </v-flex>
</template>
<script>
  export default {
    name: 'TimePickers',
    data () {
      return {
        picker: null,
        landscape: false,
        e4: null,
        time: null,
        menu2: false,
        modal2: false,
        e7: null,
        time2: '11:15'
      }
    },

    methods: {
      allowedHours: v => v % 2,
      allowedMinutes: v => v >= 10 && v <= 50
    },
    watch: {
      
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
          text: 'Time Pickers',
          disabled: true
        }
      ]
    }
  }
</script>