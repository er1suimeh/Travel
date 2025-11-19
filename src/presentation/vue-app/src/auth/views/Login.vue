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
        <v-tabs v-model="tab" bg-color="pink-accent-4" grow align-tabs="center">
          <v-tab value="login"><v-icon>mdi-login</v-icon>Login</v-tab>
          <v-tab value="register">
            <v-icon>mdi-account-box-outline</v-icon>
            Register
          </v-tab>
        </v-tabs>

        <v-window v-model="tab">
          <v-window-item value="login">
            <v-card-text>
              <form @submit.prevent="onSubmit">
                <v-row>
                  <v-col cols="12">
                    <v-text-field
                      label="E-mail"
                      v-model="login.email"
                      @blur="touchEmail"
                      @input="touchEmail"
                      :error-messages="emailErrors"
                    />
                  </v-col>

                  <v-col cols="12">
                    <v-text-field
                      type="password"
                      label="Password"
                      v-model="login.password"
                      @blur="touchPassword"
                      @input="touchPassword"
                      :error-messages="passwordErrors"
                    />
                  </v-col>

                  <v-col cols="12" class="d-flex justify-end">
                    <v-btn color="primary" type="submit">Login</v-btn>
                  </v-col>
                </v-row>
              </form>
            </v-card-text>
          </v-window-item>
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
import { ref, computed, reactive } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import useVuelidate from "@vuelidate/core";
import validators from "@/validators";

const dialog = ref(true);
const tab = ref("login");

// FORM STATE
const login = reactive({
  email: "",
  password: "",
});

// VALIDATION
const rules = {
  login: validators.login,
};

const v$ = useVuelidate(rules, { login });

// ROUTER + STORE
const store = useStore();
const router = useRouter();

const onSubmit = async () => {
  v$.value.$touch();
  if (v$.value.$invalid) return;

  await store.dispatch("authModule/loginUserAction", login);
  router.push("/admin-dashboard");
};

const navigateHome = () => router.push("/");

const touchEmail = () => v$.value.login.email.$touch();
const touchPassword = () => v$.value.login.password.$touch();

const emailErrors = computed(() => {
  const field = v$.value.login.email;
  if (!field.$dirty) return [];

  const errors = [];
  if (field.required.$invalid) errors.push("E-mail is required");
  if (field.email.$invalid) errors.push("Must be a valid e-mail");
  return errors;
});

const passwordErrors = computed(() => {
  const field = v$.value.login.password;
  if (!field.$dirty) return [];

  const errors = [];
  if (field.required.$invalid) errors.push("Password is required");
  if (field.minLength.$invalid) errors.push("Minimum characters is 8");
  return errors;
});
</script>
