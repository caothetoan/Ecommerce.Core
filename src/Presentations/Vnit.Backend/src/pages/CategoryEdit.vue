<template>
  <v-container fluid>
    <v-flex xs12>
      <v-card class="grey lighten-4 elevation-0">
        <v-card-title>
          {{title}}
          <v-spacer></v-spacer>
          <!--<v-text-field append-icon="search" label="Search" single-line hide-details v-model="search"></v-text-field>-->
          <v-btn class="deep-orange darken-3" fab small dark @click.native="add">
            <v-icon>add</v-icon>
          </v-btn>

          <v-btn fab small class="grey" @click.native="cancel()">
            <v-icon>cancel</v-icon>
          </v-btn>
          &nbsp;
          <v-btn fab small class="purple" @click.native="save()" :disabled="!isValid">
            <v-icon>save</v-icon>
          </v-btn>
          &nbsp;

        </v-card-title>
        <v-card-text>
          <v-container @keyup.enter="save()" fluid grid-list-md>
            <v-layout row wrap>
              <v-flex xs12>
                <v-text-field name="name" label="Name" :counter="100" hint="name is required" value="Input text"
                              v-model="category.name"
                              :rules="nameRules"
                              class="input-group--focused" required></v-text-field>
              </v-flex>


              <v-flex xs12>
                <v-text-field name="description" :counter="255" label="Description" hint="description" value="Input description"
                              v-model="category.description"
                              class="input-group--focused"></v-text-field>
                <!--<tinymce
             id="description"
             :content="category.description"
             :options='{width: "100%", height: "500px" }'
             value="Input text" class="input-group--focused"></tinymce>-->
              </v-flex>
              <v-flex xs12>
                <v-select v-bind:items="categories"
                          item-text="name"
                          item-value="id"
                          max-height="auto"
                          autocomplete
                          label="Parent Category"
                          v-model="category.parentCategoryId"></v-select>
              </v-flex>
              <v-flex xs12>
                <v-text-field name="displayOrder" label="displayOrder" hint="Number between 1 to 100" v-model="category.displayOrder" class="input-group--focused"></v-text-field>
              </v-flex>
              <v-flex xs12>
                <!--<v-checkbox v-model="category.published"
    label="Published"></v-checkbox>-->
                <v-switch :label="`Published`"
                          v-model="category.published"></v-switch>
              </v-flex>
              <v-flex xs12>
                <file-upload @onUploadSuccess="onUploadSuccess" />
              </v-flex>
            </v-layout>
          </v-container>
        </v-card-text>
      </v-card>
    </v-flex>

    
    <confirm-dialog :dialog="dialog" :dialogTitle="dialogTitle" :dialogText="dialogText" @onConfirm="onConfirm" @onCancel="onCancel"></confirm-dialog>
    <v-snackbar v-if="loading===false" :right="true" :timeout="timeout" :color="mode" v-model="snackbar">
      {{ notice }}
      <v-btn dark flat @click.native="closeSnackbar">Close</v-btn>
    </v-snackbar>
  </v-container>
</template>
<script>
/* global Store */
  import ConfirmDialog from "@/components/ConfirmDialog.vue";
  import FileUpload from '@/components/FileUpload'

  import {Category} from '../models'
  import { mapState, dispatch } from 'vuex'

  import { validationMixin } from 'vuelidate'
  import { required, maxLength, email } from 'vuelidate/lib/validators'
  export default {
    components: {
      ConfirmDialog,
      FileUpload
    },
    data () {
      return {
        valid: false,
        rules: {
          name: [val => (val || '').length > 0 || 'This field is required']
        },
        name: '',
        nameRules: [
        v => !!v || 'Name is required',
        v => v.length <= 100 || 'Name must be less than 100 characters'
        ],
        email: '',
        emailRules: [
        v => !!v || 'E-mail is required',
        v => /.+@.+/.test(v) || 'E-mail must be valid'
        ],
        errors: [],
        title: '',
        categoryId: null,
        modalTitle: 'Add Category',
        modalText: 'Select the category from the list',
        dialog: false,
        dialogTitle: "Category Delete Dialog",
        dialogText: "Do you want to delete this Category?",
        orderDateMenu: false,
        shippedDateMenu: false,
        snackbarStatus: false,
        timeout: 3000,
        selectedProduct: null
      }
    },
    computed: {
      ...mapState('categories',
        {
          category: 'category',
          categories: 'categories',
          loading: 'loading',
          mode: 'mode',
          snackbar: 'snackbar',
          notice: 'notice'
        }),
       isValid () {
         return (
           this.category.name
         )
       }
      /* checkboxErrors () {
        const errors = []
        if (!this.$v.category.published.$dirty) return errors
        !this.$v.category.published.required && errors.push('You must select to continue!')
        return errors
      },
  */
    },
    methods: {
      onUploadSuccess () {
        console.log('FilePond has onUploadSuccess');

      },
      save () {
        // this.$v.$touch()
        const category = Object.assign({}, this.category)
        console.log(category)
        Store.dispatch('categories/save', category)
        .then(() => {
          Store.dispatch("categories/closeSnackBar", 2000)
          this.add()
        })
      },
      clear () {
        this.$v.$reset()
        this.name = ''
        this.description = ''
        this.parentCategoryId = null
        this.published = false
      },
      getCategoryById () {
        Store.dispatch('categories/getCategoryById', this.$route.params.id)
      },
      add() {
        this.$router.push('NewCategory')
      },
      cancel () {
        this.$router.push({ name: 'Categories' })
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
      getAllCategories () {
        Store.dispatch('categories/getCategories', { query: '', pagination: { page: 1, rowsPerPage: 1000 } })
      },
      closeSnackbar () {
        Store.commit("categories/setSnackbar", { snackbar: false });
        Store.commit("categories/setNotice", { notice: "" });
      },
    },
    created () {
      this.getAllCategories()
      this.getCategoryById()
    },
    mounted () {
      if (this.$route.params.id) {
        this.title = 'Edit Category'
      } else this.title = 'New Category'
    }
  }
</script>
