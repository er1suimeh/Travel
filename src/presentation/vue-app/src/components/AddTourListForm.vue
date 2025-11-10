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
                    required
                  />
                </v-col>

                <v-col cols="12" sm="6">
                  <v-autocomplete
                    :items="countryList"
                    label="Country"
                    v-model="bodyRequest.country"
                    required
                  />
                </v-col>

                <v-col cols="12">
                  <v-textarea
                    label="About"
                    v-model="bodyRequest.about"
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
import { reactive, ref } from "vue";
import { useStore } from "vuex";
import { getCountryList } from "@/helpers/collections";

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

// --- Methods ---
async function handleSubmit() {
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
</script>
