<script setup lang="ts">
import { onMounted, ref } from "vue";
import ArtCard from "@/components/Gallery/ArtCard.vue";
import Art from "@/entities/Art";
import ArtAccessService from "@/utils/ArtAccessService";

const allArt = ref<Art[] | null>(null);

onMounted(() => {
  ArtAccessService.getAllArt() // Get All Art
    .then((promise) => (allArt.value = promise as Art[]));
});
</script>

<template>
  <div class="w-9 mx-auto my-0">
    <div class="shrink-limit flex flex-wrap">
      <ArtCard v-for="art in allArt" :key="art.artId" :art="art" />
    </div>
  </div>
</template>

<style scoped>
.shrink-limit {
  width: max(900px, 100%);
}
</style>
