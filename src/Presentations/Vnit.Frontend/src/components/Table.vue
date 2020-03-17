<template>
  <div>
  <v-data-table class="elevation-1" :headers="headers" :items="items" :search="search"
    :pagination="pagination"
    :total-items="pagination.totalItems"
    :loading="loading"
    hide-actions>
      <template slot="headers" slot-scope="props">
          <tr>
            <th>STT</th>
            <th v-for="(header, index) in props.headers" :key="header.text"
            :class="['column', 'subheading' , index === 0? 'text-xs-left': 'text-xs-center']">
              <v-tooltip bottom>
                <span slot="activator">
                  {{ header.text }}
                </span>
                <span>
                  {{ header.text }}
                </span>
              </v-tooltip>
            </th>
            <th>Action
            </th>
          </tr>
    </template>
    <!-- <template slot="pageText" slot-scope="props">
       {{ props.index + 1 + ( pagination.page - 1 ) * pagination.rowsPerPage }} - {{ props.index + 1 + ( pagination.page ) * pagination.rowsPerPage }} of {{ pagination.totalItems }}
    </template> -->
     <!-- <template slot="headerCell" slot-scope="props">
      <v-tooltip bottom>
        <span slot="activator">
          {{ props.header.text }}
        </span>
        <span>
          {{ props.header.text }}
        </span>
      </v-tooltip>
    </template> -->
    <template class="body-2" slot="items" slot-scope="props">
      <td>
          {{ props.index + 1 + ( pagination.page - 1 ) * pagination.rowsPerPage }}
      </td>
      <td v-for="(header, index) in headers" :key="index"
          :class="[ index === 0? 'text-xs-left': 'text-xs-center', 'body-2']" v-if="header.value!==''">
        <div v-if="header.mimeType==='image'">
          <v-card-media width="50px" height="50px" :src="props.item[header.value]" />
        </div>
        <div v-else-if="header.mimeType==='html'">
          <div v-html="props.item[header.value]">
          </div>
        </div>
        <div v-else-if="header.mimeType==='datetime'">
          {{props.item[header.value] | formatDate}}
        </div>
        <div v-else-if="header.mimeType==='number'">
          {{formatPrice(props.item[header.value])}}
        </div>
        <div v-else-if="typeof props.item[header.value] === 'boolean'">
          <v-btn v-if="props.item[header.value]" color="success">Yes</v-btn>
          <v-btn v-else color="warning">No</v-btn>
        </div>
        <div v-else>
          {{renderData(props.item, header)}}
        </div>
      </td>
      <td class="text-xs-right">
        <v-btn icon small class="mx-0" @click.native="$emit('edit', props.item)">
            <v-icon color="teal">edit</v-icon>
          </v-btn>
        <v-btn icon small class="mx-0" @click.native="$emit('remove', props.item)">
            <v-icon color="red">delete</v-icon>
        </v-btn>
      </td>
    </template>
    <template slot="no-data">
     <span >
        <p class="pt-2 blue--text subheading">   <v-icon medium class="blue--text" >info</v-icon>No data</p>
      </span>
    </template>
  </v-data-table>
  <div class="text-xs-center pt-2" v-if="isNotEmpty">
    <v-pagination v-model="pagination.page" :length="pagination.pages" :total-visible="10"></v-pagination>
  </div>
</div>
</template>
<script>
import formatDate from "@/utils/formatDate";

export default {
  props: {
    headers: "",
    items: "",
    pagination: ""
  },
  data() {
    return {
      search: "",
      loading: false
    };
  },
  methods: {
    renderData: (item, header) => {
      let val = "";
      if (header.value.includes(".")) {
        const vals = header.value.split(".");
        val = vals.reduce((acc, val) => acc[val], item);
      } else {
        val = item[header.value];
      }
      // if (typeof val === "boolean") {
      //   val = val ? "Yes" : "No";
      // }
      return val;
    },
    formatPrice(value) {
      const fixed = 0;
      let val = (value / 1).toFixed(fixed).replace(".", ",");
      return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }
  },
  computed: {
    isNotEmpty() {
      return this.items && this.items.length > 0;
    }
  }
  ,
  watch: {
    pagination: {
      handler() {
       this.$emit('getItems', this.pagination);
      },
      deep: true
    }
  }
};
</script>
