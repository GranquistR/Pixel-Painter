<template>
  <DrawingCanvas ref="canvas" :pixelGrid="pixelGrid" />
  <div class="absolute z-1 p-2">
    <Button
      class="mx-2"
      label="Give Me Color!"
      @click="pixelGrid.randomizeGrid()"
    />
    <Button class="mx-2" label="Recenter" @click="canvas.recenter()" />
    <BrushSelection />
    <ColorSelection />
  </div>
</template>

<script setup lang="ts">
import DrawingCanvas from "@/components/DrawingCanvas.vue";
import { PixelGrid } from "@/entities/PixelGrid";
import { ref } from "vue";
import Button from "primevue/button";
import BrushSelection from "@/components/PainterUi/BrushSelection.vue";
import ColorSelection from "@/components/PainterUi/ColorSelection.vue";

const pixelGrid = ref<PixelGrid>(new PixelGrid(64, 64));
pixelGrid.value.randomizeGrid();
const canvas = ref();
</script>
