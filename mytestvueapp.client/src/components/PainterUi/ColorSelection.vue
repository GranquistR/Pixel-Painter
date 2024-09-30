<template>
  <Dialog
    position="bottomright"
    :visible="true"
    :closable="false"
    header="Color Select"
    :style="{ width: '11rem' }"
  >
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
  </Dialog>
</template>
<script setup lang="ts">
import { ref } from "vue";
import Button from "primevue/button";
import Tag from "primevue/tag";
import Dialog from "primevue/dialog";
import Avatar from "primevue/avatar";
import ColorPicker from "primevue/colorpicker";

const selectedColor = ref<string>("#000000");

const defaultColors = ref<string[]>([
  "#000000",
  "#FFFFFF",
  "#FF0000",
  "#00FF00",
  "#0000FF",
  "#FFFF00",
  "#FF00FF",
  "#00FFFF",
  "#C0C0C0",
  "#808080",
  "#800000",
  "#808000",
]);
</script>

<style>
.p-colorpicker-preview {
  width: 7rem !important;
  height: 2rem !important;
  border: solid 1px !important;
}
</style>
