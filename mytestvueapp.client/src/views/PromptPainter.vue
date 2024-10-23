<template>
  <div class="absolute bottom-50 bg-primary flex align-items-center justify-content-center w-full h-10rem">

  <Card class="flex m-auto align-items-center justify-content-center" >
    <template #title>Select Background Color And Canvas Resolution</template>
    <template #content>
      <label for="backgroundColorPick">Background Color:  </label>
      <ColorPicker class="w-7rem p-2" v-model="backgroundColor" id="backgroundColorPick" format="hex" @change="updateLocalStorage()" ></ColorPicker>
      <br>
      <label for="resolution">Resolution: </label>
    <InputNumber class="p-2" id="resolution" v-model="resolution" showButtons buttonLayout="horizontal" suffix=" px" :min="1" :max="64" >
    <template #incrementbuttonicon >
        <span class="pi pi-plus" />
    </template>
    <template #decrementbuttonicon>
        <span class="pi pi-minus" />
    </template>
  </InputNumber>
    </template>
</Card>




</div>
<div  class="absolute bottom-0 bg-primary flex align-items-center justify-content-center w-full h-10rem" >
<RouterLink  to="/paint"
        ><Button rounded label="Start Painting" icon="pi pi-pencil" @click="updateLocalStorage()"
      /></RouterLink>
    </div>
</template>
<script setup lang="ts">

import { RouterLink } from "vue-router";
import Button from "primevue/button";
import ColorPicker from "primevue/colorpicker";
import Card from 'primevue/card';
import InputNumber from 'primevue/inputnumber';


localStorage.setItem('backgroundColor', '#ffffff');

const resolution = defineModel('resolution' ,{default: 32});


const backgroundColor = defineModel<string>({ default: "#ffffff" });
function updateLocalStorage(){
  localStorage.setItem('backgroundColor', backgroundColor.value);
  localStorage.setItem('resolution', resolution.value.toString());

}
</script>
<style>
.p-colorpicker-preview {
  width: 7rem !important;
  height: 2rem !important;
  border: solid 1px !important;

  
}

</style>