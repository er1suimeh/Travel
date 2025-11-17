import { createRouter, createWebHistory } from "vue-router";
import HomeView from "@/views/Main/HomeView.vue";
import TourLists from "@/views/AdminDashboard/TourLists.vue";
import TourPackages from "@/views/AdminDashboard/TourPackages.vue";
import { authGuard } from "@/auth/auth.guard";

const routes = [
  {
    path: "/",
    name: "Home",
    component: HomeView,
  },
  {
    path: "/about",
    name: "About",
    component: () => import("@/views/Main/AboutView.vue"),
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: "/admin-dashboard",
    component: () => import("@/views/AdminDashboard/index.vue"),
    meta: {
      requiresAuth: true,
    },
    children: [
      {
        path: "",
        component: () => import("@/views/AdminDashboard/DefaultContent.vue"),
      },
      {
        path: "weather-forecast",
        component: () => import("@/views/AdminDashboard/WeatherForecast.vue"),
      },
      {
        path: "tour-lists",
        component: TourLists,
      },
      {
        path: "tour-packages",
        component: TourPackages,
      },
    ],
  },
  {
    path: "/login",
    component: () => import("@/auth/views/Login.vue"),
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: "/logout",
    redirect: () => {
      localStorage.clear();
      return "/login";
    },
  },
  // Vue Router 4 doesn’t use * for catch-all — use a parameter instead:
  {
    path: "/:pathMatch(.*)*",
    redirect: "/",
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  console.log("router.beforeEach init ...");
  authGuard(to, from, next);
});

export default router;
