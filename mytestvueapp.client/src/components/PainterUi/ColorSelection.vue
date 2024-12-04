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
        <Tab value="0">Default</Tab>
        <Tab value="1">Custom</Tab>
    </TabList>
    <TabPanels>
        <TabPanel value="0">
          <div class="flex flex-wrap">
      <div v-for="color in DefaultColor.getDefaultColors()" :key="color.hex">
        <div
          @click="selectedColor = color.hex"
          class="border-1 m-1 w-2rem h-2rem border-round-md"
          :style="{ backgroundColor: color.hex }"
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
      <div >
        
        <div
          @click=""
          class="border-1 m-1 w-2rem h-2rem border-round-md"
        :style="{ backgroundColor: theColor}"
        ></div>
      </div>
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
import CustomColor from "@/entities/CustomColors";
import Tabs from 'primevue/tabs';
import TabList from 'primevue/tablist';
import Tab from 'primevue/tab';
import TabPanels from 'primevue/tabpanels';
import TabPanel from 'primevue/tabpanel';

const selectedColor = defineModel<string>("color", { default: "#000000" });
const size = defineModel<number>("size", { default: 1 });


let customColors: string[] = new Array(12); 

const theColor = selectedColor;

</script>

<style>
.p-colorpicker-preview {
  width: 7rem !important;
  height: 2rem !important;
  border: solid 1px !important;
}
</style>
