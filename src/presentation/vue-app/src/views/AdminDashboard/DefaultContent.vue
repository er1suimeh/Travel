<template>
  <div>
    <div class="text-h2 my-4">Welcome to Admin Dashboard</div>
    <div class="default-content">
      <div style="margin-right: 4rem; margin-bottom: 4rem">
        <TourListCard />
      </div>
    </div>
  </div>
</template>

<script setup>
import { useStore } from "vuex";
import { onMounted } from "vue";
import TourListCard from "@/components/TourListCard.vue";

// Access Vuex store
const store = useStore();

const getTourListsAction = () =>
  store.dispatch("tourModule/getTourListsAction");

onMounted(async () => {
  // Fetch tours via Vuex
  await getTourListsAction();
});
</script>

<style scoped>
.default-content {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: flex-start;
}
</style>
