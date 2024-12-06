qwww
<template>
  <FloatingCard
    position="left"
    header="Brush Settings"
    width="13rem"
    button-icon="pi pi-palette"
    button-label=""
    :default-open="true"
  >
    
  <Tabs value="0">
    <TabList>
        <Tab value="0" @click="setCurrentPallet(0)">Default</Tab>
        <Tab value="1" @click="setCurrentPallet(1)" v-tooltip.right="'Click to add custom color. Double click to remove color.'">Custom</Tab>
    </TabList>
    <TabPanels>
        <TabPanel value="0">
          <div class="flex flex-wrap">
      <div v-for="color in DefaultColor.getDefaultColors()" :key="color.hex">
        <div
          @click="selectedColor = color.hex"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          :style="{ backgroundColor: '#' + color.hex }"
          v-tooltip.bottom="color.shortcut"
        ></div>
      </div>
      <ColorPicker class="m-1" v-model="selectedColor"></ColorPicker>
    </div>
    <div class="mt-1">Size: {{ size }}</div>

    <div class="px-2">
      <Slider
        class="mt-2"
        v-model="size"
        :min="1"
        :max="32"
        v-tooltip.bottom="'Decrease(q),Increase(w)'"
      />
    </div>
        </TabPanel>
        <TabPanel value="1">
          <div class="flex flex-wrap">
        
        <div
        id = "custom1"
          @click="updateColors('custom1',0)"
          @dblclick="deleteColor('custom1',0)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: 1'"

        ></div>

        <div
        id = "custom2"
          @click="updateColors('custom2',1)"
          @dblclick="deleteColor('custom2',1)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: 2'"
        ></div>
        <div
        id = "custom3"
          @click="updateColors('custom3',2)"
          @dblclick="deleteColor('custom3',2)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: 3'"
        ></div>
        <div
        id = "custom4"
          @click="updateColors('custom4',3)" 
          @dblclick="deleteColor('custom4',3)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: 4'"
        ></div>
        <div
        id = "custom5"
          @click="updateColors('custom5',4)"
          @dblclick="deleteColor('custom5',4)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: 5'"
        ></div>
        <div
        id = "custom6"
          @click="updateColors('custom6',5)"
          @dblclick="deleteColor('custom6',5)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: 6'"
        ></div>
        <div
        id = "custom7"
          @click="updateColors('custom7',6)"
          @dblclick="deleteColor('custom7',6)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: 7'"
        ></div>
        <div
        id = "custom8"
          @click="updateColors('custom8',7)"
          @dblclick="deleteColor('custom8',7)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: 8'"
        ></div>
        <div
        id = "custom9"
          @click="updateColors('custom9',8)"
          @dblclick="deleteColor('custom9',8)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: 9'"
        ></div>
        <div
        id = "custom0"
          @click="updateColors('custom0',9)"
          @dblclick="deleteColor('custom0',9)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: 0'"
        ></div>
        <div
        id = "custom-"
          @click="updateColors('custom-',10)"
          @dblclick="deleteColor('custom-',10)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: -'"
        ></div>
        <div
        id = "custom="
          @click="updateColors('custom=',11)"
          @dblclick="deleteColor('custom=',11)"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          v-tooltip.bottom="'Shortcut: ='"
        ></div>
      
      <ColorPicker class="m-1" v-model="selectedColor"></ColorPicker>
    </div>
    <div class="mt-1">Size: {{ size }}</div>

    <div class="px-2">
      <Slider
        class="mt-2"
        v-model="size"
        min="1"
        max="32"
        v-tooltip.bottom="'Decrease(q),Increase(w)'"
      />
    </div>
        </TabPanel>
        
    </TabPanels>
</Tabs>
   
  </FloatingCard>
</template>
<script setup lang="ts">
import FloatingCard from "./FloatingCard.vue";
import ColorPicker from "primevue/colorpicker";
import Slider from "primevue/slider";
import DefaultColor from "@/entities/DefaultColors";
import Tabs from 'primevue/tabs';
import TabList from 'primevue/tablist';
import Tab from 'primevue/tab';
import TabPanels from 'primevue/tabpanels';
import TabPanel from 'primevue/tabpanel';



const selectedColor = defineModel<string>("color", { default: "000000" });
const size = defineModel<number>("size", { default: 1 });


let customColors: string[] = new Array(12); 
let arrayDefault: string[] = new Array(12);
for(let i = 0; i < arrayDefault.length; i++){
  arrayDefault[i]=DefaultColor.getDefaultColors()[i].hex;
}


function updateColors(id: string, index: number){
  let unique = true
  for (let i=0; i < customColors.length; i++){
    if(customColors[i]===selectedColor.value)
    unique=false;
  }
  if(unique){
  if(!customColors[index]){
  customColors[index]=selectedColor.value;
  const currentCustom= document.getElementById(id);
  if(currentCustom){
   currentCustom.style.backgroundColor = '#' + selectedColor.value;
  }
}
}
if(customColors[index]){
    selectedColor.value=customColors[index];
  }
  setCurrentPallet(1);
}

function deleteColor( id: string, index: number){
  customColors[index]="";
  const currentCustom= document.getElementById(id);
  if(currentCustom){
   currentCustom.style.backgroundColor = "transparent";
  }

}

function setCurrentPallet(tab: number){
if(tab === 0){
  localStorage.setItem('currentPallet', JSON.stringify(arrayDefault))
}
else
  localStorage.setItem('currentPallet', JSON.stringify(customColors))
}

</script>

<style>
.p-colorpicker-preview {
  width: 7rem !important;
  height: 2rem !important;
  border: solid 1px !important;
}
</style>
