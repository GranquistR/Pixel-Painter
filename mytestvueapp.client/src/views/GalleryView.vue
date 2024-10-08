<script setup lang="ts">
import { onMounted, ref } from "vue";
import ArtCard from "@/components/Gallery/ArtCard.vue";
import Art from "@/entities/Art";
import ArtAccessService from "@/utils/ArtAccessService";

const allArt = ref<Art[] | null>(null); 
const particularArt = ref<Art | null>(null);


onMounted(() => {
  ArtAccessService.getAllArt() // Get All Art
  .then((promise) => allArt.value = promise as Art[]);
  ArtAccessService.getArtById(1) // Get a particular art
  .then((promise) => particularArt.value = promise as Art);
});

</script>

<template>
  <div class="w-9 mx-auto my-0">
    <div class="flex flex-wrap">
      <ArtCard v-for="art in allArt" :key="art.artId" :art="art" />
      <ArtCard v-if="particularArt" :art="particularArt" />
    </div>
  </div>
</template>
