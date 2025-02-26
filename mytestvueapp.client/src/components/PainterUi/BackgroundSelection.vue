<template>
  <FloatingCard position="left"
  header="Background Select"
  width="13rem"
  button-icon="pi pi-tablet"
  button-label=""
  :default-open="false">
    <Tabs value="0">
      <TabPanels>
        <TabPanel value="0">
          <div class="flex flex-wrap">
            <div v-for="color in DefaultColor.getDefaultColors()"
                 :key="color.hex">
              <div @click="selectedColor = color.hex"
                   class="border-1 m-1 w-2rem h-2rem border-round-md"
                   :style="{ backgroundColor: '#' + color.hex }"
                   v-tooltip.bottom="color.shortcut"></div>
            </div>
            <!-- @input="updateColorFromHex" -->
            <div class="parent">
              <input class="pl-1"
                     v-model="hexColor"
                     placeholder="#000000"
                     style="width: 54%"
                     @focus="inputActive"
                     @blur="inputInactive"
                     @input="ValidateHex"
                     @keydown.enter="handleEnter" />
            </div>
            <ColorPicker class="m-1" v-model="selectedColor" />
          </div>
        </TabPanel>
        <TabPanel value="1">
          <div class="flex flex-wrap">
            <div id="1"
                 @click="updateColors('1', 0)"
                 @dblclick="deleteColor('1', 0)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + 1'"></div>

            <div id="2"
                 @click="updateColors('2', 1)"
                 @dblclick="deleteColor('2', 1)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + 2'"></div>
            <div id="3"
                 @click="updateColors('3', 2)"
                 @dblclick="deleteColor('3', 2)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + 3'"></div>
            <div id="4"
                 @click="updateColors('4', 3)"
                 @dblclick="deleteColor('4', 3)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + 4'"></div>
            <div id="5"
                 @click="updateColors('5', 4)"
                 @dblclick="deleteColor('5', 4)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + 5'"></div>
            <div id="6"
                 @click="updateColors('6', 5)"
                 @dblclick="deleteColor('6', 5)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + 6'"></div>
            <div id="7"
                 @click="updateColors('7', 6)"
                 @dblclick="deleteColor('7', 6)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + 7'"></div>
            <div id="8"
                 @click="updateColors('8', 7)"
                 @dblclick="deleteColor('8', 7)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + 8'"></div>
            <div id="9"
                 @click="updateColors('9', 8)"
                 @dblclick="deleteColor('9', 8)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + 9'"></div>
            <div id="0"
                 @click="updateColors('0', 9)"
                 @dblclick="deleteColor('0', 9)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + 0'"></div>
            <div id="-"
                 @click="updateColors('-', 10)"
                 @dblclick="deleteColor('-', 10)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut: ctrl + -'"></div>
            <div id="="
                 @click="updateColors('=', 11)"
                 @dblclick="deleteColor('=', 11)"
                 class="border-1 m-1 w-2rem h-2rem border-round-md"
                 v-tooltip.bottom="'Shortcut:ctrl +  ='"></div>
            <div class="parent">
              <input class="pl-1"
                     v-model="hexColor"
                     placeholder="#000000"
                     style="width: 54%"
                     @focus="inputActive"
                     @blur="inputInactive"
                     @input="ValidateHex"
                     @keydown.enter="handleEnter" />
            </div>
            <ColorPicker class="m-1" v-model="selectedColor"></ColorPicker>
          </div>
        </TabPanel>
      </TabPanels>
    </Tabs>
  </FloatingCard>
</template>

<script setup lang="ts">
import { ref, defineEmits, watch } from "vue";
import FloatingCard from "./FloatingCard.vue";
import ColorPicker from "primevue/colorpicker";
import DefaultColor from "@/entities/DefaultColors";
import Tabs from "primevue/tabs";
import TabPanels from "primevue/tabpanels";
import TabPanel from "primevue/tabpanel";
import Button from "primevue/button";

const selectedColor = defineModel<string>("color", { default: "FFFFFF" });
const hexColor = ref<string>("#000000");
const emit = defineEmits(["DisableKeyBinds", "EnableKeyBinds"]);

let arrayDefault: string[] = new Array(12);
for (let i = 0; i < arrayDefault.length; i++) {
  arrayDefault[i] = DefaultColor.getDefaultColors()[i].hex;
}

watch(selectedColor, () => {
  hexColor.value = "#" + selectedColor.value;
});

function handleEnter(event: KeyboardEvent) {
  (event.target as HTMLElement).blur();
}
function inputActive() {
  emit("DisableKeyBinds");
}
function inputInactive() {
  emit("EnableKeyBinds");
  if (hexColor.value.length == 7) {
    selectedColor.value = hexColor.value.substring(1, 7);
  }
}
function ValidateHex() {
  hexColor.value =
    "#" + hexColor.value.replace(/[^0-9a-fA-F]/g, "").toUpperCase();
  if (hexColor.value.length > 7) {
    hexColor.value = hexColor.value.substring(0, 7);
  }
}
</script>
