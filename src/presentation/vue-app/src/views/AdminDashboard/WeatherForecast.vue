<template>
  <v-container>
    <div class="text-h4 mb-10">
      Two-week weather forecast of different cities
    </div>

    <!-- Loading spinner -->
    <div class="v-picker--full-width d-flex justify-center" v-if="loading">
      <v-progress-circular :size="70" :width="7" color="purple" indeterminate />
    </div>

    <!-- City selector -->
    <v-select
      v-model="selectedCity"
      :items="cities"
      label="City"
      persistent-hint
      return-object
      single-line
      clearable
      class="mb-6"
      @update:modelValue="fetchWeatherForecast"
    />

    <!-- Table (Vuetify 3 version of v-simple-table) -->
    <v-table class="elevation-1 rounded-lg">
      <thead>
        <tr>
          <th class="text-left">Dates</th>
          <th class="text-left">City</th>
          <th class="text-left">℃</th>
          <th class="text-left">℉</th>
          <th class="text-left">Summary</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in weatherForecast" :key="item.date">
          <td>{{ item.date }}</td>
          <td>{{ item.city }}</td>
          <td>{{ item.temperatureC }}</td>
          <td>{{ item.temperatureF }}</td>
          <td>
            <v-chip
              :color="getColor(item.summary)"
              variant="flat"
              text-color="white"
              class="text-capitalize"
            >
              {{ item.summary }}
            </v-chip>
          </td>
        </tr>
      </tbody>
    </v-table>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from "vue";
import dayjs from "dayjs";
import relativeTime from "dayjs/plugin/relativeTime";
import { getWeatherForecastV2Axios } from "@/api/weather-forecast-services";

// Enable relative time formatting
dayjs.extend(relativeTime);

// Reactive state
const weatherForecast = ref([]);
const selectedCity = ref("Oslo");
const loading = ref(false);
const cities = ref([]);
//const lists = ref([]); // holds your tours

// Fetch weather data
async function fetchWeatherForecast(city = "Oslo") {
  loading.value = true;
  try {
    const { data } = await getWeatherForecastV2Axios(city);
    weatherForecast.value = data?.map((w) => {
      const formattedData = { ...w };
      let date = w.date;
      formattedData.date = dayjs(date).fromNow();
      return formattedData;
    });
  } catch (err) {
    alert("Something happened. Please try again.");
  } finally {
    loading.value = false;
  }
}

// Pick color for summary
function getColor(summary) {
  switch (summary) {
    case "Freezing":
      return "indigo";
    case "Bracing":
      return "blue";
    case "Chilly":
      return "light-blue";
    case "Cool":
      return "cyan";
    case "Mild":
      return "teal";
    case "Warm":
      return "yellow";
    case "Balmy":
      return "amber";
    case "Hot":
      return "orange";
    case "Sweltering":
      return "deep-orange";
    case "Scorching":
      return "red";
    default:
      return "grey";
  }
}

// Mount logic
onMounted(async () => {
  loading.value = true;
  await fetchWeatherForecast(selectedCity.value);
  loading.value = false;
});
</script>
