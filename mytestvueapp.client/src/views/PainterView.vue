
<template>
  <DrawingCanvas ref="canvas" :pixelGrid="pixelGrid" />
  <div class="absolute z-1 p-2">
    <Button
      class="mr-2"
      label="Give Me Color!"
      @click="pixelGrid.randomizeGrid()"
    />
    <Button label="Recenter" class="mr-2" @click="canvas.recenter()" />
    <Button label="Encode painting" class="mr-2" @click="printEncodedText" />
    <Button label="Decode Painting" class="mr-2" @click="decodeToCanvas" />
    
    
    <div class="mr-2"style="height:100px;width:140px;overflow:auto;word-wrap: break-word;padding:10px;">{{stringEncodedText}}</div>
    
    <Textarea class="mr-2" name ='textArea' style="height:100px;width:140px;" placeholder ="Copy and paste encoded text here and press decode."/>
    
    
    
  </div>
 
  
    
  
</template>

<script setup lang="ts">
import DrawingCanvas from "@/components/DrawingCanvas.vue";
import { PixelGrid } from "@/entities/PixelGrid";
import codec from "@/utils/codec";
import { ref, Text } from "vue";
import Button from "primevue/button";
import Textarea from 'primevue/textarea';

const pixelGrid = ref<PixelGrid>(new PixelGrid(64, 64));
pixelGrid.value.randomizeGrid();

let stringEncodedText= ref('Encoded text will show here.');

const printEncodedText = () => {
  stringEncodedText.value = codec.Encode(pixelGrid.value);
}

const decodeToCanvas = () => {
  console.log(stringEncodedText.value);
  let decodedPixelGrid = <PixelGrid>(new PixelGrid(64, 64));
  decodedPixelGrid=codec.Decode(stringEncodedText.value,64,64);
  pixelGrid.value.updateGrid(decodedPixelGrid);
 
}


const canvas = ref();
</script>
