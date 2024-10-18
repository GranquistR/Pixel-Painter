<template>
  <div class="fixed top-0  justify-content-between align-items-center">
<FloatingCard class="justify-content-between align-items-center" position="center" header="Select Background Color" width="17rem">
  
    <ColorPicker class="w-7rem" v-model="backgroundColor" id="backgroundColorPick" format="hex" @change="updateLocalStorage()"></ColorPicker>
    
</FloatingCard>
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
import FloatingCard from "./../components/PainterUi/FloatingCard.vue";

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