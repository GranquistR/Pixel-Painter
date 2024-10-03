<template>
  {{ cursorPosition }}
  <DrawingCanvas
    ref="canvas"
    :pixelGrid="pixelGrid"
    :selectedColor="selectedColor"
    :selectedTool="selectedTool"
    :style="{ cursor: selectedTool.cursor }"
    v-model="cursorPosition"
    @mousedown="mouseButtonHeldDown = true"
    @mouseup="mouseButtonHeldDown = false"
    @contextmenu.prevent
  />
  <div class="absolute z-1 p-2">
    <Button
      class="mx-2"
      label="Give Me Color!"
      @click="pixelGrid.randomizeGrid()"
    />

    <Button class="mx-2" label="Recenter" @click="canvas?.recenter()" />
    <BrushSelection v-model="selectedTool" />
    <ColorSelection v-model="selectedColor" />
    
    <Button label="Encode painting" class="mr-2" @click="printEncodedText" />
    <Button label="Decode Painting" class="mr-2" @click="decodeToCanvas" />
    
    
    <div class="mr-2"style="height:100px;width:140px;overflow:auto;word-wrap: break-word;padding:10px;">{{stringEncodedText}}</div>
    
    <Textarea class="mr-2" name ='textArea' style="height:100px;width:140px;" placeholder ="Copy and paste encoded text here and press decode."/>
    
    
    

  </div>
 
  
    
  
</template>

<script setup lang="ts">
import DrawingCanvas from "@/components/PainterUi/DrawingCanvas.vue";
import { PixelGrid } from "@/entities/PixelGrid";
import codec from "@/utils/codec";

import { ref, watch } from "vue";
import Button from "primevue/button";
import BrushSelection from "@/components/PainterUi/BrushSelection.vue";
import ColorSelection from "@/components/PainterUi/ColorSelection.vue";
import PainterTool from "@/entities/PainterTool";
import { Vector2 } from "@/entities/Vector2";

var selectedTool = ref<PainterTool>(PainterTool.getDefaults()[1]);
var selectedColor = ref<string>("#000000");
var cursorPosition = ref<Vector2>(new Vector2(0, 0));
var mouseButtonHeldDown = ref<boolean>(false);

const pixelGrid = ref<PixelGrid>(new PixelGrid(32, 32));

import { ref, Text } from "vue";

import Textarea from 'primevue/textarea';


watch(cursorPosition.value, async () => {
  if (mouseButtonHeldDown.value) {
    if (selectedTool.value.label === "Brush") {
      pixelGrid.value.grid[cursorPosition.value.x][cursorPosition.value.y] =
        selectedColor.value;
    } else if (selectedTool.value.label === "Eraser") {
      pixelGrid.value.grid[cursorPosition.value.x][cursorPosition.value.y] =
        "#FFFFFF";
    }
  }
});

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
