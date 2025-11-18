<template>
  <div class="container" v-if="email">
    <div class="text-h4 my-5">Do you like to continue as {{ email }}? 🔒</div>

    <v-btn color="primary" class="mr-4" @click="onSubmit">Yes</v-btn>

    <v-btn variant="outlined" color="primary" @click="handleLogOut">
      No thanks, I'd like to log out
    </v-btn>
  </div>

  <div v-else class="text-h4 m-10">
    Fancy meeting you here.

    <v-btn variant="outlined" color="primary" @click="handleLogOut">
      Go to Home Page
    </v-btn>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { getUserEmailFromToken, logOut } from "@/auth/auth.service";

const router = useRouter();

// state
const email = ref(getUserEmailFromToken());

// methods
function handleLogOut() {
  logOut();
}

function onSubmit() {
  router.push("/admin-dashboard");
}
</script>
