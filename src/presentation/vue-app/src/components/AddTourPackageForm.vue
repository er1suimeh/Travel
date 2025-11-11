<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600">
      <!-- Modern activator slot -->
      <template #activator="{ props }">
        <v-btn class="mt-4" rounded="xl" color="pink" v-bind="props">
          <v-icon start>mdi-plus</v-icon>
          Add New Tour Package
        </v-btn>
      </template>

      <v-card>
        <v-form @submit.prevent="onSubmit">
          <v-card-title class="text-h6 font-weight-medium">
            Create New Tour Package
          </v-card-title>

          <v-card-text>
            <v-container>
              <v-row dense>
                <v-col cols="12">
                  <v-text-field label="Name" v-model="form.name" required />
                </v-col>

                <v-col cols="12">
                  <v-textarea
                    label="What to Expect"
                    v-model="form.whatToExpect"
                    auto-grow
                    required
                  />
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    label="Map Location"
                    v-model="form.mapLocation"
                    required
                  />
                </v-col>

                <v-col cols="12" sm="4">
                  <v-text-field
                    label="Price (USD)"
                    v-model.number="form.price"
                    type="number"
                    min="1"
                    required
                  />
                </v-col>

                <v-col cols="12" sm="4">
                  <v-select
                    :items="durations"
                    v-model="form.duration"
                    label="Duration (hours)"
                  />
                </v-col>

                <v-col cols="12" sm="4">
                  <v-switch
                    label="Instant Confirmation"
                    v-model="form.instantConfirmation"
                    color="primary"
                  />
                </v-col>
              </v-row>
            </v-container>

            <small class="text-grey-darken-1">* Required fields</small>
          </v-card-text>

          <v-card-actions>
            <v-spacer />
            <v-btn variant="text" color="secondary" @click="dialog = false">
              Close
            </v-btn>
            <v-btn variant="flat" color="blue" type="submit">Save</v-btn>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script setup>
import { ref } from "vue";
import { useStore } from "vuex";

// Props
const props = defineProps({
  tourListId: Number,
});

// Store
const store = useStore();

// State
const dialog = ref(false);
const durations = [1, 2, 3, 4, 5, 6, 7, 8];

const form = ref({
  listId: 0,
  name: "",
  whatToExpect: "",
  mapLocation: "https://www.google.com/maps/place/...",
  price: 10,
  duration: 1,
  instantConfirmation: true,
});

// Methods
async function onSubmit() {
  form.value.listId = props.tourListId;
  await store.dispatch("tourModule/addTourPackageAction", form.value);
  dialog.value = false;

  // reset form fields
  form.value = {
    listId: 0,
    name: "",
    whatToExpect: "",
    mapLocation: "https://www.google.com/maps/place/...",
    price: 10,
    duration: 1,
    instantConfirmation: true,
  };
}
</script>
