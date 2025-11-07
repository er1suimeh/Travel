<template>
  <!-- Skeleton Loader -->
  <v-skeleton-loader
    v-if="loading"
    type="card"
    width="300"
    max-width="450"
    height="100%"
  />

  <!-- Tour List Card -->
  <v-card v-else width="300" max-width="450" height="100%">
    <v-toolbar color="light-blue" dark>
      <v-toolbar-title>Tour Lists</v-toolbar-title>
      <v-spacer />
    </v-toolbar>

    <!-- Vuetify 3: simplified list syntax -->
    <v-list lines="two" density="comfortable">
      <v-list-item
        v-for="tourList in lists"
        :key="tourList.id"
        :title="tourList.city"
        :subtitle="tourList.about"
        @click="addToPackages(tourList.tourPackages, tourList.id)"
      >
        <template #append>
          <div class="mr-2">
            {{ tourList.tourPackages?.length || 0 }}
          </div>
          <v-icon
            icon="mdi-delete-outline"
            @click.stop="removeTourList(tourList.id)"
          />
        </template>
      </v-list-item>
    </v-list>
  </v-card>
</template>

<script setup>
import { computed } from "vue";
import { useStore } from "vuex";

// Access Vuex store
const store = useStore();

// Getters and actions from Vuex
const lists = computed(() => store.getters["tourModule/lists"]);
const loading = computed(() => store.getters["tourModule/loading"]);
</script>
