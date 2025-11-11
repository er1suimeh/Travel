<template>
  <div class="inline-block">
    <!-- Trigger button -->
    <v-icon
      class="mr-3 cursor-pointer"
      icon="mdi-clipboard-edit-outline"
      @click.stop="dialog = true"
    />

    <!-- Dialog -->
    <v-dialog v-model="dialog" persistent max-width="600px">
      <v-card>
        <form @submit.prevent="onSubmit">
          <v-card-title>
            <span class="headline">Update Tour Package</span>
          </v-card-title>

          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field
                    label="Name"
                    v-model="localBody.name"
                    required
                  />
                </v-col>

                <v-col cols="12">
                  <v-textarea
                    label="What to expect"
                    v-model="localBody.whatToExpect"
                    required
                  />
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    label="Map Location"
                    v-model="localBody.mapLocation"
                    required
                  />
                </v-col>

                <v-col cols="12" sm="4">
                  <v-text-field
                    label="Price in USD"
                    v-model="localBody.price"
                    type="number"
                    required
                  />
                </v-col>

                <v-col cols="12" sm="4">
                  <v-autocomplete
                    :items="durations"
                    v-model="localBody.duration"
                    label="Duration (hrs)"
                  />
                </v-col>

                <v-col cols="12" sm="4">
                  <v-switch
                    label="Instant confirmation"
                    v-model="localBody.instantConfirmation"
                  />
                </v-col>
              </v-row>
            </v-container>
            <small>* indicates required field</small>
          </v-card-text>

          <v-card-actions>
            <v-spacer />
            <v-btn text color="blue darken-1" @click="dialog = false">
              Close
            </v-btn>
            <v-btn text color="blue darken-1" type="submit">Update</v-btn>
          </v-card-actions>
        </form>
      </v-card>
    </v-dialog>
  </div>
</template>

<script setup>
import { ref, watch } from "vue";
import { useStore } from "vuex";

const props = defineProps({
  bodyRequest: {
    type: Object,
    required: true,
    default: () => ({
      listId: 0,
      name: "missing name",
      whatToExpect: "missing what to expect",
      mapLocation: "missing map location",
      price: 0,
      duration: 0,
      instantConfirmation: true,
    }),
  },
});

const emit = defineEmits(["updated"]);
const store = useStore();

const dialog = ref(false);
const durations = [1, 2, 3, 4, 5, 6, 7, 8];

// Make a local copy to avoid prop mutation
const localBody = ref({ ...props.bodyRequest });

// Sync when prop changes
watch(
  () => props.bodyRequest,
  (newVal) => {
    localBody.value = { ...newVal };
  },
  { deep: true }
);

async function onSubmit() {
  try {
    await store.dispatch("tourModule/updateTourPackageAction", localBody.value);
    emit("updated", localBody.value);
    dialog.value = false;
  } catch (err) {
    console.error("Error updating tour package:", err);
  }
}
</script>
