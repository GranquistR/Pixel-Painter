qwww
<template>
  <FloatingCard
    position="left"
    header="Brush Settings"
    width="11rem"
    button-icon="pi pi-palette"
    button-label=""
    :default-open="true"
  >
    Color:
    <div class="flex flex-wrap">
      <div v-for="color in DefaultColor.getDefaultColors()" :key="color.hex">
        <div
          @click="selectedColor = color.hex"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          :style="{ backgroundColor: color.hex }"
          v-tooltip.bottom="color.shortcut"
        ></div>
      </div>
      <ColorPicker class="m-1" v-model="selectedColor"></ColorPicker>
    </div>
    <div class="mt-1">Size: {{ size }}</div>

    <div class="px-2">
      <Slider
        class="mt-2"
        v-model="size"
        :min="1"
        :max="32"
        v-tooltip.bottom="'Decrease(q),Increase(w)'"
      />
    </div>
  </FloatingCard>
</template>
<script setup lang="ts">
import FloatingCard from "./FloatingCard.vue";
import ColorPicker from "primevue/colorpicker";
import Slider from "primevue/slider";
import DefaultColor from "@/entities/DefaultColors";

const selectedColor = defineModel<string>("color", { default: "#000000" });
const size = defineModel<number>("size", { default: 1 });
</script>

<style>
.p-colorpicker-preview {
  width: 7rem !important;
  height: 2rem !important;
  border: solid 1px !important;
}
</style>
