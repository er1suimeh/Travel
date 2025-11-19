<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <!-- Activator Button -->
      <template #activator="{ props }">
        <v-btn class="mt-4" rounded color="light-blue" dark v-bind="props">
          <v-icon start>mdi-plus</v-icon>
          Add new tour list
        </v-btn>
      </template>

      <!-- Dialog Card -->
      <v-card>
        <v-form @submit.prevent="handleSubmit">
          <v-card-title>
            <span class="headline">Create New Tour List</span>
          </v-card-title>

          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12" sm="6">
                  <v-text-field
                    label="City"
                    v-model="bodyRequest.city"
                    @blur="touchCity"
                    @input="touchCity"
                    :error-messages="cityErrors"
                    required
                  />
                </v-col>

                <v-col cols="12" sm="6">
                  <v-autocomplete
                    :items="countryList"
                    label="Country"
                    v-model="bodyRequest.country"
                    @blur="touchCountry"
                    @input="touchCountry"
                    :error-messages="countryErrors"
                    required
                  />
                </v-col>

                <v-col cols="12">
                  <v-textarea
                    label="About"
                    v-model="bodyRequest.about"
                    @blur="touchAbout"
                    @input="touchAbout"
                    :error-messages="aboutErrors"
                    required
                  />
                </v-col>
              </v-row>
            </v-container>
            <small>* indicates required field</small>
          </v-card-text>

          <v-card-actions>
            <v-spacer />
            <v-btn color="blue darken-1" text @click="dialog = false">
              Close
            </v-btn>
            <v-btn color="blue darken-1" text type="submit">Save</v-btn>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script setup>
import { reactive, ref, computed } from "vue";
import { useStore } from "vuex";
import { getCountryList } from "@/helpers/collections";
import validators from "@/validators";
import useVuelidate from "@vuelidate/core";

// --- Vuex Store ---
const store = useStore();

// --- Reactive State ---
const dialog = ref(false);
const bodyRequest = reactive({
  city: "",
  country: "",
  about: "",
});
const countryList = getCountryList();

const rules = {
  bodyRequest: {
    city: validators.city,
    country: validators.country,
    about: validators.about,
  },
};

const v$ = useVuelidate(rules, { bodyRequest });

// --- Methods ---
async function handleSubmit() {
  v$.value.$touch();
  if (v$.value.$invalid) return;

  try {
    await store.dispatch("tourModule/addTourListAction", { ...bodyRequest });
    //alert("Tour list added successfully!");
    dialog.value = false;

    // Reset form fields
    bodyRequest.city = "";
    bodyRequest.country = "";
    bodyRequest.about = "";
  } catch (e) {
    console.error(e);
    //alert("Failed to add tour list.");
  }
}

const touchCity = () => v$.value.bodyRequest.city.$touch();
const touchCountry = () => v$.value.bodyRequest.country.$touch();
const touchAbout = () => v$.value.bodyRequest.about.$touch();

const cityErrors = computed(() => {
  const field = v$.value.bodyRequest.city;
  if (!field.$dirty) return [];

  const errors = [];
  if (field.required.$invalid) errors.push("City is required");
  if (field.maxLength.$invalid) errors.push("Max length is 90 characters");
  return errors;
});

const countryErrors = computed(() => {
  const field = v$.value.bodyRequest.country;
  if (!field.$dirty) return [];

  const errors = [];
  if (field.required.$invalid) errors.push("Country is required");
  return errors;
});
const aboutErrors = computed(() => {
  const field = v$.value.bodyRequest.about;
  if (!field.$dirty) return [];

  const errors = [];
  if (field.required.$invalid) errors.push("About is required");
  return errors;
});
</script>
