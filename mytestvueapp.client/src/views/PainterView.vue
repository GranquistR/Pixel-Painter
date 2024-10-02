<template>
  {{ cursorPosition }}
  <DrawingCanvas
    ref="canvas"
    :pixelGrid="pixelGrid"
    :selectedColor="selectedColor"
    :selectedTool="selectedTool"
    :style="{ cursor: selectedTool.cursor }"
    v-model="cursorPosition"
    @mousedown="mouseButtonHeldDown = true"
    @mouseup="mouseButtonHeldDown = false"
    @contextmenu.prevent
  />
  <div class="absolute z-1 p-2">
    <Button
      class="mx-2"
      label="Give Me Color!"
      @click="pixelGrid.randomizeGrid()"
    />
    <Button class="mx-2" label="Recenter" @click="canvas?.recenter()" />
    <BrushSelection v-model="selectedTool" />
    <ColorSelection v-model="selectedColor" />
    <Button
      label="Encode painting"
      class="mr-2"
      @click="codec.Encode(pixelGrid)"
    />
    <Button
      label="Decode Painting"
      class="mr-2"
      @click="codec.Decode('', 16, 16)"
    />
  </div>
</template>

<script setup lang="ts">
import DrawingCanvas from "@/components/DrawingCanvas.vue";
import { PixelGrid } from "@/entities/PixelGrid";
import codec from "@/utils/codec";
import { ref, watch } from "vue";
import Button from "primevue/button";
import BrushSelection from "@/components/PainterUi/BrushSelection.vue";
import ColorSelection from "@/components/PainterUi/ColorSelection.vue";
import PainterTool from "@/entities/PainterTool";
import { Vector2 } from "@/entities/Vector2";

var selectedTool = ref<PainterTool>(PainterTool.getDefaults()[1]);
var selectedColor = ref<string>("#000000");
var cursorPosition = ref<Vector2>(new Vector2(0, 0));
var mouseButtonHeldDown = ref<boolean>(false);

const pixelGrid = ref<PixelGrid>(new PixelGrid(32, 32));

watch(cursorPosition.value, async () => {
  if (mouseButtonHeldDown.value) {
    if (selectedTool.value.label === "Brush") {
      pixelGrid.value.grid[cursorPosition.value.x][cursorPosition.value.y] =
        selectedColor.value;
    } else if (selectedTool.value.label === "Eraser") {
      pixelGrid.value.grid[cursorPosition.value.x][cursorPosition.value.y] =
        "#FFFFFF";
    }
  }
});

const canvas = ref();
</script>
