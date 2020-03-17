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
          <v-btn fab small class="purple" @click.native="save()">
            <v-icon>save</v-icon>
          </v-btn>
          &nbsp;

        </v-card-title>
        <v-card-text>
          <v-container @keyup.enter="save()" fluid grid-list-md >
            <v-layout row wrap>
              <v-flex xs12>
                <v-text-field name="name" label="name" hint="name is required" value="Input text" v-model="newsItem.name"
                              class="input-group--focused" required></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field name="short" label="short" hint="short is required" value="Input text" v-model="newsItem.short"
                              class="input-group--focused"></v-text-field>
              </v-flex>

              <v-flex xs12>
                <v-text-field name="displayOrder" label="displayOrder" hint="Number between 1 to 100" v-model="newsItem.displayOrder" class="input-group--focused"></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-select required v-bind:items="newscategories"
                          label="News Category" v-model="newsItem.newsCategoryIds"></v-select>
              </v-flex>
              <v-flex md4 xs12>
                <v-menu lazy :close-on-content-click="false" transition="v-scale-transition" offset-y full-width
                        :nudge-left="40" max-width="290px">
                  <v-text-field slot="activator" label="Start Date" v-model="newsItem.startDateUtc" prepend-icon="event" readonly></v-text-field>
                  <v-date-picker v-model="newsItem.startDateUtc" no-title scrollable>
                  </v-date-picker>
                </v-menu>
              </v-flex>
              <v-flex md4 xs12>
                <v-menu lazy :close-on-content-click="false" transition="v-scale-transition" offset-y full-width
                        :nudge-left="40" max-width="290px">
                  <v-text-field slot="activator" label="End Date" v-model="newsItem.endDateUtc" prepend-icon="event" readonly></v-text-field>
                  <v-date-picker v-model="newsItem.endDateUtc" no-title scrollable>
                  </v-date-picker>
                </v-menu>
              </v-flex>
              <v-flex xs12>
                <file-upload @onUploadSuccess="onUploadSuccess" />
              </v-flex>
              <v-flex xs12>
                <tinymce id="full" :content="newsItem.full"
                         :options='{width: "100%", height: "500px" }'
                         hint="full is required" value="Input text" class="input-group--focused"></tinymce>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card-text>
      </v-card>
    </v-flex>    
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
  import FileUpload from '@/components/FileUpload'

  import { article } from '../models'
  import { mapState, dispatch } from 'vuex'

  export default {
    components: {
      ConfirmDialog,
      FileUpload
    },
    data () {
      return {
        categoryId: null,
        modalTitle: 'Add article',
        modalText: 'Select the category from the list',
        addProductModal: false,
        dialog: false,
        dialogTitle: "Product Delete Dialog",
        dialogText: "Do you want to delete this article?",
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
      ...mapState('articles',
        {
          newsItem: 'article',
          newscategories: 'newscategories',
          loading: 'loading',
          mode: 'mode',
          snackbar: 'snackbar',
          notice: 'notice'
        }),
    },
    methods: {
      save () {
        const article = { ...this.article }
        console.log(article)
        Store.dispatch('articles/save', article)
        .then(() => {
          Store.dispatch("articles/closeSnackBar", 2000)
        })
      },
      getArticleById () {
        Store.dispatch('articles/getArticleById', this.$route.params.id)
      },
      getCategories() {
        Store.dispatch('articles/getCategories', { query: '', pagination: { page: 1, rowsPerPage: 1000 } })
      },
      add() {
        this.$router.push({ name: 'NewArticle' })
      },
      cancel () {
        this.$router.push({ name: 'Articles' })
      },
      remove (item) {
        this.selectedProduct = item;
        this.dialog = true;
      },
      onConfirm () {
        Store.dispatch(
          "articles/deleteProduct", Object.assign({}, this.selectedProduct)
        );
        this.selectedProduct = null;
        this.dialog = false;
      },
      onCancel () {
        this.selectedProduct = null;
        this.dialog = false;
      },
      closeSnackbar () {
        Store.commit("articles/setSnackbar", { snackbar: false });
        Store.commit("articles/setNotice", { notice: "" });
      },
      onUploadSuccess() {
        console.log('FilePond has onUploadSuccess');

      },
    },
    created () {
      this.getCategories()
      this.getArticleById()
    },
    mounted () {
      if (this.$route.params.id) {
        this.title = 'Edit article'
      } else this.title = 'New article'
    }
  }
</script>
