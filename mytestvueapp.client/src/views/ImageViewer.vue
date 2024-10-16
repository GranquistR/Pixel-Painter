<template>
  <div class="p-8 justify-content-center flex w-full h-full align-items-center">
    <div class="border-2">
      <my-canvas
        v-if="allArt"
        :key="allArt.artId"
        :art="allArt"
        :pixelSize="10"
      ></my-canvas>
    </div>
  </div>
  <div class="flex justify-content-center align-items-center">
  </div>
  <div class="border-solid border-1">
    <h2>this is some text</h2>
  </div>
</template>
<script setup lang="ts">
import Art from "@/entities/Art";
import MyCanvas from "@/components/MyCanvas/MyCanvas.vue";
import { ref, onMounted } from "vue";
import Button from "primevue/button";
import ArtAccessService from "../utils/ArtAccessService";
const allArt = ref<Art | null>(null);

const MyImg = ref<Art>();
onMounted(() => {
  ArtAccessService.getArtById(1).then(
    (promise) => (allArt.value = promise as Art),
  );
});
</script>
