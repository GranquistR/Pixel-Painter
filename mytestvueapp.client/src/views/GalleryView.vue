<script setup lang="ts">
import { onMounted, ref, watch } from "vue";
import ArtCard from "@/components/Gallery/ArtCard.vue";
import Art from "@/entities/Art";
import ArtAccessService from "@/services/ArtAccessService";
import { PixelGrid } from "@/entities/PixelGrid";

const allArt = ref<Art[] | null>(null);
const displayArt = ref<Art[] | null>(null);
const search = ref("")


onMounted(() => {
  ArtAccessService.getAllArt() // Get All Art
    .then((promise) => (
      allArt.value = promise as Art[],
      displayArt.value = promise as Art[]
    ));
});


watch(search, () =>{
  if (allArt.value){
    displayArt.value = allArt.value.filter(Art => Art.artName.toLowerCase().includes(search.value.toLowerCase()))
  }
})

</script>

<template>
  <div class="w-9 mx-auto my-0">
    <header>
      <h1 class="flex align-items-center gap-3">Search for Art
      <input class="mt-2 " v-model.trim="search" type="text" placeholder="Search..." size="30" width="100%">
      </h1>
    </header>
    <div class="shrink-limit flex flex-wrap">
      <ArtCard v-for="art in displayArt" :key="art.artId" :art="art" />
    </div>
  </div>
</template>

<style scoped>
.shrink-limit {
  width: max(900px, 100%);
}
</style>
