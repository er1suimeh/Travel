<template>
  <v-app>
    <v-dialog
      v-model="dialog"
      persistent
      max-width="600"
      min-width="360"
      @click:outside="navigateHome"
    >
      <v-card>
        <!-- TABS -->
        <v-tabs v-model="tab" bg-color="pink-accent-4" grow align-tabs="center">
          <v-tab value="login">
            <v-icon>mdi-login</v-icon>
            Login
          </v-tab>

          <v-tab value="register">
            <v-icon>mdi-account-box-outline</v-icon>
            Register
          </v-tab>
        </v-tabs>

        <!-- TAB PANELS (Vuetify 3 style) -->
        <v-window v-model="tab">
          <!-- LOGIN TAB -->
          <v-window-item value="login">
            <v-card-text>
              <form @submit.prevent="onSubmit">
                <v-row>
                  <v-col cols="12">
                    <v-text-field label="E-mail" v-model="login.email" />
                  </v-col>

                  <v-col cols="12">
                    <v-text-field
                      type="password"
                      label="Password"
                      hint="At least 8 characters"
                      counter
                      v-model="login.password"
                    />
                  </v-col>

                  <v-col cols="12" class="d-flex justify-end">
                    <v-btn color="primary" type="submit">Login</v-btn>
                  </v-col>
                </v-row>
              </form>
            </v-card-text>
          </v-window-item>

          <!-- REGISTER TAB -->
          <v-window-item value="register">
            <v-card-text>
              <v-row>
                <v-col cols="12" sm="6">
                  <v-text-field label="First Name" required maxlength="20" />
                </v-col>

                <v-col cols="12" sm="6">
                  <v-text-field label="Last Name" required maxlength="20" />
                </v-col>

                <v-col cols="12">
                  <v-text-field label="E-mail" required />
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    label="Password"
                    hint="At least 8 characters"
                    counter
                  />
                </v-col>

                <v-col cols="12">
                  <v-text-field label="Confirm Password" counter />
                </v-col>

                <v-col cols="12" class="d-flex justify-end">
                  <v-btn color="primary">Register</v-btn>
                </v-col>
              </v-row>
            </v-card-text>
          </v-window-item>
        </v-window>
      </v-card>
    </v-dialog>
  </v-app>
</template>

<script setup>
import { ref } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";

const dialog = ref(true);
const tab = ref("login");

const login = ref({
  email: "",
  password: "",
});

// vuex + router
const store = useStore();
const router = useRouter();

const onSubmit = async () => {
  await store.dispatch("authModule/loginUserAction", login.value);
  router.push("/admin-dashboard");
};

const navigateHome = () => {
  router.push("/");
};
</script>
