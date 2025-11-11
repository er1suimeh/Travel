<template>
  <v-container>
    <v-card width="500" max-width="600">
      <v-toolbar color="pink" dark>
        <v-toolbar-title>Packages</v-toolbar-title>
        <v-spacer />
      </v-toolbar>

      <v-list lines="two">
        <!-- When packages exist -->
        <template v-if="packages.length">
          <template v-for="tourPackage in packages" :key="tourPackage.id">
            <v-list-item
              :title="tourPackage.name"
              :subtitle="tourPackage.whatToExpect"
            >
              <template #append>
                <div class="flex flex-row space-x-2">
                  <span class="text-caption">
                    Duration: {{ tourPackage.duration }} hrs
                  </span>
                  <span
                    v-if="tourPackage.instantConfirmation"
                    class="text-caption text-secondary"
                  >
                    Instant Confirmation
                  </span>
                </div>

                <v-icon
                  icon="mdi-delete-outline"
                  @click="removeTourPackage(tourPackage.id)"
                />
              </template>
            </v-list-item>

            <v-divider />
          </template>
        </template>

        <!-- When there are no packages -->
        <template v-else>
          <v-list-item title="No package added yet 😢" />
        </template>
      </v-list>
    </v-card>
  </v-container>
</template>

<script setup>
import { computed } from "vue";
import { useStore } from "vuex";

const store = useStore();

const packages = computed(
  () => store.getters["tourModule/packagesOfSelectedCity"]
);

function removeTourPackage(packageId) {
  const confirmed = confirm(
    "Are you sure you want to permanently delete this tour package?"
  );
  if (!confirmed) return;

  store.dispatch("tourModule/removeTourPackageAction", packageId);
}
</script>

<style scoped>
.flex {
  display: flex;
}
.flex-row {
  flex-direction: row;
}
.space-x-2 > * + * {
  margin-left: 0.5rem;
}
</style>
