<template>
   <v-container fluid>
        <v-flex xs12>
             <v-card>
                <v-card-title>
                    <span class="title">Categories {{pagination? "("+pagination.totalItems+")": ""}}
                    <v-text-field append-icon="search" label="Quick Search" single-line hide-details v-model="quickSearchData"></v-text-field>
                    </span>
                    <v-spacer></v-spacer>
                    <v-btn class="blue-grey" fab small dark @click.native.stop="rightDrawer = !rightDrawer">
                    <v-icon>search</v-icon>
                    </v-btn>
                    <v-btn class="brown lighten-1" fab small dark @click.native="reloadData()">
                    <v-icon>refresh</v-icon>
                    </v-btn>
                    <v-btn class="teal darken-2" fab small dark @click.native="print()">
                    <v-icon>print</v-icon>
                    </v-btn>
                    <v-btn class="deep-orange darken-3" fab small dark @click.native="add">
                    <v-icon>add</v-icon>
                    </v-btn>
                </v-card-title>
                <Table v-if="loading===false" :headers="headers" :items="items"  :pagination="pagination" @edit="edit" @remove="remove"></Table>
             </v-card>

        </v-flex>

         <search-panel :rightDrawer="rightDrawer" @cancelSearch="cancelSearch" @searchData="searchData">
        <v-layout row>
          <v-flex xs11 offset-xs1>
            <v-text-field name="name" label="Reference" light v-model="searchVm.contains.name"></v-text-field>
          </v-flex>
        </v-layout>

      </search-panel>
    <confirm-dialog :dialog="dialog" :dialogTitle="dialogTitle" :dialogText="dialogText" @onConfirm="onConfirm" @onCancel="onCancel" ></confirm-dialog>
    <v-snackbar v-if="loading===false" :right="true" :timeout="timeout" :color="mode" v-model="snackbar" >
      {{ notice }}
      <v-btn dark flat @click.native="closeSnackbar">Close</v-btn>
    </v-snackbar>
    </v-container>
</template>
<script>
/* globals Store */
import Table from "@/components/Table.vue";
import SearchPanel from "@/components/SearchPanel.vue";
import ConfirmDialog from "@/components/ConfirmDialog.vue";
import { mapState } from "vuex";
import { debounce } from "lodash";

export default {
  components: {
    Table,
    SearchPanel,
    ConfirmDialog
  },
 data () {
    return {
      dialog: false,
      dialogTitle: "Category Delete Dialog",
      dialogText: "Do you want to delete this category?",
      rightDrawer: false,
      right: true,
      searchData: '',
      headers: [
        { text: 'Name', value: 'cat_name' },
        { text: 'Desc', value: 'descrip' },
        { text: 'picture', value: 'picture' },
        { text: 'display Order', value: 'displayOrder' }
      ],
      searchVm: {
        contains: {
          name: '',
          customer: ''
        },
        between: {
          amount: { former: 0, latter: 0 }
        }
      },
      orderId: "",
      query: "",
      snackbarStatus: false,
      timeout: 2000,
      color: "",
      quickSearchFilter: "",
    }
  },
  methods: {
    print () {
      window.print()
    },
    edit (item) {
      this.$router.push({ name: 'Category', params: { id: item.id } })
    },
    add () {
      this.$router.push('NewCategory')
    },
    remove (item) {
      this.orderId = item.id;
      this.dialog = true;
    },
    onConfirm () {
      Store.dispatch(
        "categories/delete", this.orderId).then(() => {
        Store.dispatch("categories/search", this.query, this.pagination);
        Store.dispatch("categories/closeSnackBar", 2000);
      });
      this.dialog = false;
    },
    onCancel () {
      this.orderId = "";
      this.dialog = false;
    },
    search () {
      this.rightDrawer = !this.rightDrawer;
      this.appUtil.buildSearchFilters(this.searchVm);
      this.query = this.appUtil.buildJsonServerQuery(this.searchVm);
      this.quickSearch = "";
      Store.dispatch("categories/search", this.query, this.pagination);
    },
    reloadData () {
      this.query = "";
      Store.dispatch("categories/getAll");
    },
    cancelSearch () {
      this.rightDrawer = false;
    },
    closeSnackbar () {
      Store.commit("categories/setSnackbar", { snackbar: false });
      Store.commit("categories/setNotice", { notice: "" });
    },
    quickSearch: debounce(function () {
      console.log(this.quickSearchFilter)
      this.quickSearchFilter && Store.dispatch("categories/quickSearch",
       { headers: this.headers,
         qsFilter: this.quickSearchFilter.toLowerCase(),
         pagination: this.pagination });
    }, 300),
  },
  computed: {
    ...mapState("categories", {
      items: "items",
      pagination: "pagination",
      loading: "loading",
      mode: "mode",
      snackbar: "snackbar",
      notice: "notice"
    }),
    quickSearchData: {
      get: function () {
        return this.quickSearchFilter;
      },
      set: function ( val ) {
        this.quickSearchFilter = val;
        this.quickSearchFilter && this.quickSearch();
      }
    }
  },
  created () {
    Store.dispatch("categories/getAll");
  },
  mounted () {
    this.$nextTick(() => {
      console.log(this.headers);
    })
  }
}
</script>