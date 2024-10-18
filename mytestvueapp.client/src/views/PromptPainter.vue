<template>
<FloatingCard position="center" header="Select Background Color" width="17rem">
  <div class="flex justify-content-between align-items-center  px-6 py-4">
  
    <ColorPicker class="m-1" v-model="backgroundColor" id="backgroundColorPick" format="hex" @change="updateLocalStorage()"></ColorPicker>
    
    
    
  
  </div>
</FloatingCard>
<div  id="startPainting">
<RouterLink class="p-2" to="/paint"
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
  margin: 0 auto;
}
#startPainting{
  position: fixed;
  left: 50%;
  bottom: 20px;
  transform: translate(-50%, -50%);
  margin: 0 auto;
}
</style>