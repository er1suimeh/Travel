<template>
  <v-sheet height="100vh" class="d-flex flex-row overflow-hidden">
    <!-- Navigation Drawer -->
    <v-navigation-drawer
      permanent
      expand-on-hover
      color="surface"
      class="elevation-2"
    >
      <!-- User Info -->
      <v-list class="pa-4 d-flex flex-column align-center">
        <v-avatar size="64">
          <v-img src="https://randomuser.me/api/portraits/women/85.jpg" />
        </v-avatar>
        <div class="mt-3 text-caption text-center text-medium-emphasis">
          email@example.com
        </div>
      </v-list>

      <v-divider class="my-2"></v-divider>

      <!-- Navigation Links -->
      <v-list nav density="comfortable">
        <router-link
          v-for="link in links"
          :key="link.to"
          :to="link.to"
          class="menu-link"
        >
          <v-list-item :value="link.title">
            <template #prepend>
              <v-icon>{{ link.icon }}</v-icon>
            </template>
            <v-list-item-title>{{ link.title }}</v-list-item-title>
          </v-list-item>
        </router-link>
      </v-list>
    </v-navigation-drawer>

    <!-- Main Content -->
    <v-main class="pa-6 bg-grey-lighten-4">
      <router-view />
    </v-main>
  </v-sheet>
</template>

<script setup>
defineOptions({ name: "AdminDashboard" });

// Navigation links
const links = [
  {
    title: "Dashboard",
    icon: "mdi-view-dashboard-variant-outline",
    to: "/admin-dashboard/",
  },
  {
    title: "Tour Lists",
    icon: "mdi-format-list-bulleted",
    to: "/admin-dashboard/tour-lists",
  },
  {
    title: "Tour Packages",
    icon: "mdi-walk",
    to: "/admin-dashboard/tour-packages",
  },
  {
    title: "Weather Forecast",
    icon: "mdi-weather-cloudy-alert",
    to: "/admin-dashboard/weather-forecast",
  },
  {
    title: "Log out",
    icon: "mdi-logout-variant",
    to: "/logout",
  },
];
</script>

<style scoped>
.menu-link {
  text-decoration: none;
  color: inherit;
}

.v-navigation-drawer {
  width: 260px;
  border-right: 1px solid rgba(0, 0, 0, 0.08);
}

.v-list-item:hover {
  background-color: rgba(0, 0, 0, 0.04);
  transition: background-color 0.2s ease-in-out;
}

.v-main {
  flex: 1;
  overflow-y: auto;
}
</style>
