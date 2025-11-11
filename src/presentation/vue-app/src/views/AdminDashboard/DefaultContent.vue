<template>
  <div>
    <div class="text-h2 my-4">Welcome to Admin Dashboard</div>
    <div class="default-content">
      <div style="margin-right: 4rem; margin-bottom: 4rem">
        <TourListCard @handleShowPackages="handleShowPackages" />
        <AddTourListForm />
      </div>
      <div v-if="showPackages">
        <TourPackagesCard />
        <AddTourPackageForm :tourListId="tourListId" />
      </div>
    </div>
  </div>
</template>

<script setup>
import { useStore } from "vuex";
import { onMounted, ref } from "vue";
import TourListCard from "@/components/TourListCard.vue";
import AddTourListForm from "@/components/AddTourListForm.vue";
import TourPackagesCard from "@/components/TourPackagesCard.vue";
import AddTourPackageForm from "@/components/AddTourPackageForm.vue";

// Access Vuex store
const store = useStore();

const showPackages = ref(false);
const tourListId = ref(0);

const getTourListsAction = () =>
  store.dispatch("tourModule/getTourListsAction");

onMounted(async () => {
  // Fetch tours via Vuex
  await getTourListsAction();
  showPackages.value = false;
});

function handleShowPackages(show, listId) {
  showPackages.value = show;
  tourListId.value = listId;
}
</script>

<style scoped>
.default-content {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: flex-start;
}
</style>
