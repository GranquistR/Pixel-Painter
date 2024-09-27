<template>
  <DrawingCanvas ref="canvas" :pixelGrid="pixelGrid" />
  <div class="absolute z-1 p-2">
    <Button
      class="mr-2"
      label="Give Me Color!"
      @click="pixelGrid.randomizeGrid()"
    />
    <Button label="Recenter" @click="canvas.recenter()" />
    <Button label="Encode painting" @click="pixelGrid.Encode()"/>
  </div>
</template>

<script setup lang="ts">
import DrawingCanvas from "@/components/DrawingCanvas.vue";
import { PixelGrid } from "@/entities/PixelGrid";
import { ref } from "vue";
import Button from "primevue/button";

const pixelGrid = ref<PixelGrid>(new PixelGrid(16, 16));
const canvas = ref();
</script>
