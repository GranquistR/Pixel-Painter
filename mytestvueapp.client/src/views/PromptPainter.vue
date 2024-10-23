<template>
  <div class="absolute bottom-50 bg-primary flex align-items-center justify-content-center w-full h-10rem">

    <Card >
    <template #title>Enter Title And Description Of Art</template>
    <template #content>
      <InputText class="mt-2 w-full" type="text" v-model="title" placeholder="Title"/>
      <Textarea class="mt-2 w-full" name="textArea" placeholder="Description" v-model="description"/>
    </template>
</Card>

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
import Textarea from "primevue/textarea";
import InputText from 'primevue/inputtext';



localStorage.setItem('backgroundColor', '#ffffff');
const title = defineModel<string>('title', {default:""});
const description = defineModel<string>('description',{default:""});
const backgroundColor = defineModel<string>({ default: "#ffffff" });
function updateLocalStorage(){
  localStorage.setItem('backgroundColor', backgroundColor.value);
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