<template>
  <DrawingCanvas ref="canvas" :pixelGrid="pixelGrid" />
  <div class="absolute z-1 p-2">
    <Button
      class="mr-2"
      label="Give Me Color!"
      @click="pixelGrid.randomizeGrid()"
    />
    <Button label="Recenter" @click="canvas.recenter()" />
  </div>
</template>

<script setup lang="ts">
import DrawingCanvas from "@/components/DrawingCanvas.vue";
import { PixelGrid } from "@/entities/PixelGrid";
import { ref } from "vue";
import Button from "primevue/button";

const pixelGrid = ref<PixelGrid>(new PixelGrid(64, 64));
pixelGrid.value.randomizeGrid();
const canvas = ref();
</script>
