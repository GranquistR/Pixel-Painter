<template>
  <div class="w-9 mx-auto my-0">
    <header>
      <h1 class="flex align-items-center gap-3">
        Search for Art
        <InputText
          class="mt-2"
          v-model.trim="search"
          type="text"
          placeholder="Search..."
          size="30"
          width="100%"
        />
      </h1>
    </header>
    <div class="shrink-limit flex flex-wrap" v-if="!loading">
      <ArtCard v-for="art in displayArt" :key="art.id" :art="art" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref, watch } from "vue";
import ArtCard from "@/components/Gallery/ArtCard.vue";
import Art from "@/entities/Art";
import ArtAccessService from "@/services/ArtAccessService";
import InputText from "primevue/inputtext";

const allArt = ref<Art[]>();
const displayArt = ref<Art[]>();
const search = ref("");
const loading = ref(true);

onMounted(() => {
  ArtAccessService.getAllArt() // Get All Art
    .then((data) => {
      console.log(data);
      allArt.value = data;
      displayArt.value = data;
    })
    .finally(() => {
      loading.value = false;
    });
});

watch(search, () => {
  if (allArt.value) {
    displayArt.value = allArt.value.filter((Art) =>
      Art.title.toLowerCase().includes(search.value.toLowerCase())
    );
  }
});
</script>

<style scoped>
.shrink-limit {
  width: max(900px, 100%);
}
</style>
