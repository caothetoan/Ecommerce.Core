<template>
  <v-container fluid>
    <v-flex xs12>
      <v-card class="grey lighten-4 elevation-0">
        <v-card-title>
          {{title}}
          <v-spacer></v-spacer>
          <v-btn class="deep-orange darken-3" fab small dark @click.native="add">
            <v-icon>add</v-icon>
          </v-btn>
          <!--<v-text-field append-icon="search" label="Search" single-line hide-details v-model="search"></v-text-field>-->
          <v-btn fab small class="grey" @click.native="cancel()">
            <v-icon>cancel</v-icon>
          </v-btn>
          &nbsp;
          <v-btn fab small class="purple" @click.native="save()" :disabled="!isValid">
            <v-icon>save</v-icon>
          </v-btn>
        </v-card-title>
        <v-card-text>
          <v-container @keyup.enter="save()" fluid grid-list-md>
            <v-layout row wrap class="px-10">
              <v-flex xs12>
                <v-text-field name="name" label="Product" hint="Product name is required" v-model="product.name"
                              class="input-group--focused" required :rules="rules.name"></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field mask name="price" type="number" label="Price" hint="Price is required" v-model="product.price"
                              class="input-group--focused" required></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field name="stockQuantity" type="number" label="Quantity" hint="Quantity between 1 to 1000" v-model="product.stockQuantity" class="input-group--focused"
                              required></v-text-field>
              </v-flex>

              <v-flex xs12>
                <v-autocomplete required v-bind:items="categories"
                          item-text="name"
                          item-value="id"
                          max-height="auto"
                          multiple
                          chip
                          label="Category" v-model="product.categoryIds" :rules="rules.category"></v-autocomplete>
              </v-flex>
              <v-flex xs12>
                <v-switch :label="`Published`"
                          v-model="product.published"></v-switch>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card-text>
      </v-card>
    </v-flex>
    <v-snackbar v-if="loading===false" :right="true" :timeout="timeout" :color="mode" v-model="snackbar" >
      {{ notice }}
      <v-btn dark flat @click.native="closeSnackbar">Close</v-btn>
    </v-snackbar>
  </v-container>
</template>
<script>
import {Product} from '../models'
import { mapState, dispatch } from 'vuex'
/* global Store */
export default {
  data () {
    return {
      errors: [],
      title: '',
      snackbarStatus: false,
      timeout: 3000,
      color: '',
      rules: {
        name: [val => (val || '').length > 0 || 'This field is required'],
        category: [val => this.product.categoryIds !== '' || 'This field is required']
      }

    }
  },
  methods: {
    save () {
      const product = Object.assign({}, this.product)
      delete product.category

      Store.dispatch('products/saveProduct', product)
        .then(() => {
          Store.dispatch("products/closeSnackBar", 2000)
          this.$router.push({ name: 'NewProduct' })
        })
    },
    selectCategory (item) {
      this.product.categoryId = item.value
    },
    getProduct () {
      Store.dispatch('products/getProductById', this.$route.params.id)
    },
    getCategories () {
      Store.dispatch('products/getCategories', { query: '', pagination: { page: 1, rowsPerPage: 1000 } })
    },
    cancel () {
      this.$router.push({ name: 'Products' })
    },
    add() {
      this.$router.push({ name: 'NewProduct' })
    }
  },
  computed: {
        ...mapState('products',
        {
          product: 'product',
          categories: 'categories',
          loading: 'loading',
          mode: 'mode',
          snackbar: 'snackbar',
          notice: 'notice'
        }),
        isValid () {
          return (
            this.product.categoryIds  && this.product.name
          )
        }
    },
  created () {
    this.getCategories()
    this.getProduct()
  },
  mounted () {
      if (this.$route.params.id) {
        this.title = 'Edit Product'
      } else this.title = 'New Product'
  }
}
</script>
