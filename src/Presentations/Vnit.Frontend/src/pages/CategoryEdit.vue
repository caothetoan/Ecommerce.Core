<template>
  <v-container fluid>
    <v-flex xs12>
      <v-card class="grey lighten-4 elevation-0">
        <v-card-title>
          {{title}}
          <v-spacer></v-spacer>
          <!--<v-text-field append-icon="search" label="Search" single-line hide-details v-model="search"></v-text-field>-->
          <v-btn fab small class="grey" @click.native="cancel()">
            <v-icon>cancel</v-icon>
          </v-btn>
          &nbsp;
          <v-btn fab small class="purple" @click.native="save()">
            <v-icon>save</v-icon>
          </v-btn>
          &nbsp;
          <v-btn fab small class="blue" @click.native="addProduct()">
            <v-icon>add</v-icon>
          </v-btn>
        </v-card-title>
        <v-card-text>
          <v-container fluid grid-list-md >
            <v-layout row wrap >
              <v-flex md4 xs12>
                <v-text-field name="name" label="name" hint="name is required" value="Input text" v-model="category.name"
                  class="input-group--focused" required></v-text-field>
              </v-flex>



            </v-layout>
          </v-container>
        </v-card-text>
      </v-card>
    </v-flex>

    <v-layout row justify-center>
      <v-dialog v-model="addProductModal" width="700" persistent>
        <v-card>
          <v-card-title>{{modalTitle}}</v-card-title>
          <v-card-text>{{modalText}}</v-card-text>
          <v-card-text>
            <v-container fluid grid-list-md>
              <v-layout row wrap>
                <v-flex md6 xs12>
                  <v-select required v-bind:items="categories" label="Category" v-model="categoryId"  v-on:change="getProductsByCategory"></v-select>
                </v-flex>
                <v-flex md6 xs12>
                  <v-select required v-bind:items="products" label="Product" v-model="productId"></v-select>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-btn class="green--text darken-1" flat="flat" @click.native="saveProduct">Confirm</v-btn>
            <v-btn class="green--text darken-1" flat="flat" @click.native="cancelAddProduct">Cancel</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-layout>
      <confirm-dialog :dialog="dialog" :dialogTitle="dialogTitle" :dialogText="dialogText" @onConfirm="onConfirm" @onCancel="onCancel" ></confirm-dialog>
    <v-snackbar v-if="loading===false" :right="true" :timeout="timeout" :color="mode" v-model="snackbar" >
      {{ notice }}
      <v-btn dark flat @click.native="closeSnackbar">Close</v-btn>
    </v-snackbar>
  </v-container>
</template>
<script>
/* global Store */
import ConfirmDialog from "@/components/ConfirmDialog.vue";
import {Category} from '../models'
import { mapState, dispatch } from 'vuex'
  export default {
    components: {
      ConfirmDialog
    },
    data () {
      return {
        categoryId: null,
        modalTitle: 'Add Category',
        modalText: 'Select the category from the list',
        addProductModal: false,
        dialog: false,
        dialogTitle: "Category Delete Dialog",
        dialogText: "Do you want to delete this Category?",
        orderDateMenu: false,
        shippedDateMenu: false,
        errors: [],
        title: '',
        productId: null,
        snackbarStatus: false,
        timeout: 3000,
        color: '',
        selectedProduct: null
      }
    },
    computed: {
      ...mapState('categories',
        {
          categories: 'categories',
          loading: 'loading',
          mode: 'mode',
          snackbar: 'snackbar',
          notice: 'notice'
        }),
    },
    methods: {
      save () {
        const order = { ...this.order }
        delete order.customer
        console.log(order)
        Store.dispatch('categories/save', order)
        .then(() => {
          Store.dispatch("categories/closeSnackBar", 2000)
        })
      },

      getById () {
        Store.dispatch('categories/getById', this.$route.params.id)
      },

      cancel () {
        this.$router.push({ name: 'categories' })
      },
      remove (item) {
        this.selectedProduct = item;
        this.dialog = true;
      },
      onConfirm () {
        Store.dispatch(
          "categories/delete", Object.assign({}, this.selectedProduct)
        );
        this.selectedProduct = null;
        this.dialog = false;
      },
      onCancel () {
        this.selectedProduct = null;
        this.dialog = false;
      },
      addProduct () {
        this.addProductModal = true
      },

      getCategories () {
        Store.dispatch('categories/getCategories')
      },

      closeSnackbar () {
        Store.commit("categories/setSnackbar", { snackbar: false });
        Store.commit("categories/setNotice", { notice: "" });
      },
    },
    created () {
      this.getCategories()
    },
    mounted () {
      if (this.$route.params.id) {
        this.title = 'Edit Category'
      } else this.title = 'New Category'
    }
  }
</script>
