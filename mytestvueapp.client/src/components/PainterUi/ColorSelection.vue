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
      <div v-for="color in defaultColors" :key="color">
        <div
          @click="selectedColor = color"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          :style="{ backgroundColor: color }"
        ></div>
      </div>
      <ColorPicker class="m-1" v-model="selectedColor"></ColorPicker>
    </div>
    <div class="mt-1">Size: {{ size }}</div>

    <div class="px-2">
      <Slider class="mt-2" v-model="size" min="1" max="32" />
    </div>
  </FloatingCard>
</template>
<script setup lang="ts">
import { ref } from "vue";
import FloatingCard from "./FloatingCard.vue";
import ColorPicker from "primevue/colorpicker";
import Slider from "primevue/slider";

const selectedColor = defineModel<string>("color", { default: "#000000" });
const size = defineModel<number>("size", { default: 1 });

const defaultColors = ref<string[]>([
  "#000000",
  "#ED1C24",
  "#FF7F27",
  "#7F7F7F",
  "#FFF200",
  "#22B14C",
  "#C3C3C3",
  "#00A2E8",
  "#3F48CC",
  "#FFFFFF",
  "#A349A4",
  "#FFAEC9",
]);
</script>

<style>
.p-colorpicker-preview {
  width: 7rem !important;
  height: 2rem !important;
  border: solid 1px !important;
}
</style>
