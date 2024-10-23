<template>
  <div class="absolute bottom-50 bg-primary flex align-items-center justify-content-center w-full h-10rem">


    <Card >
    <template #title>Enter Title And Description Of Art</template>
    <template #content>
      <InputText class="mt-2 w-full" type="text" v-model="title" placeholder="Title"/>
      <Textarea class="mt-2 w-full" name="textArea" placeholder="Description" v-model="description"/>
    </template>
</Card>


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
import Textarea from "primevue/textarea";
import InputText from 'primevue/inputtext';

localStorage.setItem('backgroundColor', '#ffffff');

const resolution = defineModel('resolution' ,{default: 32});
const title = defineModel<string>('title', {default:""});
const description = defineModel<string>('description',{default:""});

const backgroundColor = defineModel<string>({ default: "#ffffff" });
function updateLocalStorage(){
  localStorage.setItem('backgroundColor', backgroundColor.value);
  localStorage.setItem('resolution', resolution.value.toString());
  localStorage.setItem('title', title.value);
  localStorage.setItem('description', description.value);

}
</script>
<style>
.p-colorpicker-preview {
  width: 7rem !important;
  height: 2rem !important;
  border: solid 1px !important;

  
}

</style>