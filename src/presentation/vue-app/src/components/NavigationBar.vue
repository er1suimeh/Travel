<template>
  <v-app-bar color="primary" theme="dark" density="comfortable" elevation="2">
    <div class="d-flex align-center">
      <router-link to="/" class="menu d-flex align-center">
        <v-img
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-logo-dark.png"
          alt="Vuetify Logo"
          width="40"
          class="mr-3"
          cover
        />
      </router-link>
    </div>

    <v-spacer />

    <div class="d-flex align-center ga-2">
      <v-btn variant="outlined" color="white" :to="{ path: '/' }">Home</v-btn>

      <router-link to="/about" class="menu">
        <v-btn variant="outlined" color="white">About</v-btn>
      </router-link>

      <v-btn
        v-if="isAuthenticated"
        variant="outlined"
        color="white"
        :to="{ path: '/admin-dashboard' }"
      >
        Dashboard
      </v-btn>

      <v-btn v-else variant="outlined" color="white" :to="{ path: '/login' }">
        Login
      </v-btn>
    </div>
  </v-app-bar>
</template>

<script setup>
import { computed } from "vue";
import { useStore } from "vuex";

const store = useStore();

const isAuthenticated = computed(
  () => store.getters["authModule/isAuthenticated"]
);

//const email = computed(() => store.getters["authModule/email"]);
</script>

<style scoped>
.menu {
  color: white;
  text-decoration: none;
}
</style>
