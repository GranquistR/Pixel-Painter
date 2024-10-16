<template>
<div class="flex justify-content-between align-items-center  px-6 py-4">

    <h1>Select color: </h1>
    <ColorPicker class="m-1" v-model="backgroundColor" id="backgroundColorPick"></ColorPicker>
    <RouterLink class="p-2" to="/paint"
        ><Button rounded label="Start Painting" icon="pi pi-pencil"
      /></RouterLink>
    
</div>
</template>
<script setup lang="ts">
import { RouterLink } from "vue-router";
import Button from "primevue/button";
import ColorPicker from "primevue/colorpicker";

const backgroundColor = defineModel<string>({ default: "#000000" });

</script>
<style>
.p-colorpicker-preview {
  width: 7rem !important;
  height: 2rem !important;
  border: solid 1px !important;
}
</style>