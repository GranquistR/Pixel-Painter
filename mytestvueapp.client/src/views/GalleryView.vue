<script setup lang="ts">
import { onMounted, ref } from "vue";
import ArtCard from "@/components/Gallery/ArtCard.vue";
import Art from "@/entities/Art";
import ArtAccessService from "@/utils/ArtAccessService";
import { PixelGrid } from "@/entities/PixelGrid";

const allArt = ref<Art[] | null>(null);
const artpiece = ref<Art | null>(null);

onMounted(() => {
  ArtAccessService.getAllArt() // Get All Art
    .then((promise) => (allArt.value = promise as Art[]));
    ArtAccessService.getArtById(1) // Get All Art
    .then((promise) => (artpiece.value = promise as Art));
});
</script>

<template>
  <div class="w-9 mx-auto my-0">
    <div class="shrink-limit flex flex-wrap">
      <ArtCard v-for="art in allArt" :key="art.artId" :art="art" />
      <ArtCard v-if="artpiece" :key="artpiece.artId" :art="artpiece" />
      <div v-for="art in allArt" :key="art.artId">{{ art.pixelGrid }}</div>
      <!-- {{ allArt?[1].pixelGrid }} -->
      {{ "ArtPiece" + artpiece?.pixelGrid }} 
      
    </div>
  </div>
</template>

<style scoped>
.shrink-limit {
  width: max(900px, 100%);
}
</style>
