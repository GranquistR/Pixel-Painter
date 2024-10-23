<template>
  <div class="absolute bottom-50 bg-primary flex align-items-center justify-content-center w-full h-10rem">

  <Card >
    <template #title>Select Background Color</template>
    <template #content>
      <ColorPicker class="w-7rem" v-model="backgroundColor" id="backgroundColorPick" format="hex" @change="updateLocalStorage()"></ColorPicker>
    </template>
</Card>


</div>
<div  class="absolute bottom-0 bg-primary flex align-items-center justify-content-center w-full h-10rem" >
<RouterLink  to="/paint"
        ><Button rounded label="Start Painting" icon="pi pi-pencil"
      /></RouterLink>
    </div>
</template>
<script setup lang="ts">

import { RouterLink } from "vue-router";
import Button from "primevue/button";
import ColorPicker from "primevue/colorpicker";
import Card from 'primevue/card';


localStorage.setItem('backgroundColor', '#ffffff');

const backgroundColor = defineModel<string>({ default: "#ffffff" });
function updateLocalStorage(){
  localStorage.setItem('backgroundColor', backgroundColor.value);
}
</script>
<style>
.p-colorpicker-preview {
  width: 7rem !important;
  height: 2rem !important;
  border: solid 1px !important;

  
}

</style>