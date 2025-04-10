<template>
  <FloatingCard position="bottom"
                header="Layers"
                button-icon="pi pi-clone"
                button-label=""
                width=""
                :default-open="false">

    <Button class="mr-1" :disabled="layers.length == 1" icon="pi pi-minus" size="small" rounded @click="popLayer()" />

    <template v-for="layer in layers">
      <Button icon="pi pi-stop"
              :class="['m-1', { 'selected-layer': layer === selectedLayer } ]"
              severity="secondary"
              @click="switchLayer(layer)"
              @contextmenu.prevent="deleteLayer(layer)"/>
    </template>

    <Button class="ml-1" :disabled="layers.length==8" icon="pi pi-plus" size="small" rounded @click="pushLayer()" />
  </FloatingCard>
</template>

<script setup lang="ts">
import Button from "primevue/button";
import FloatingCard from "./FloatingCard.vue";
import { useLayerStore } from "@/store/LayerStore"
import { PixelGrid } from "@/entities/PixelGrid"

import { ref, onBeforeMount, watch } from 'vue';

const props = defineProps<{
  updateLayers: number;
}>();

const layerStore = useLayerStore();
const selectedLayer = ref<number>(0);
const layers = ref<number[]>([0]);

onBeforeMount(() => {
  for (let i = 1; i < layerStore.grids.length; i++)
    layers.value.push(i);
});

function pushLayer() {
  if (layers.value.length < 8) {
    layers.value.push(layers.value.length);
    layerStore.pushGrid(new PixelGrid(
      layerStore.grids[0].height,
      layerStore.grids[0].height,
      layerStore.grids[0].backgroundColor,
      layerStore.grids[0].isGif)
    );
  }
}

function deleteLayer(idx: number) {
  if (layers.value.length > 1) {
    const isConfirmed = confirm("This will delete layer " + (idx+1) + ". Are you sure you want to delete this layer?");
    if (!isConfirmed) return;

    if (idx === -1) {
      console.warn(`Layer with id ${(idx+1)} not found`);
      return;
    }
    //remove specific layer
    layers.value.splice(idx, 1);
    layerStore.removeGrid(idx);


    layers.value.forEach((layer, index) => {
      layers.value[index] = index;
    });

    if (!layers.value.some(layer => layer === selectedLayer.value)) {
      selectedLayer.value = layers.value[layers.value.length - 1] ?? 1;
      layerStore.layer = selectedLayer.value;
    } else {
      selectedLayer.value--;
      layerStore.layer--;
    }
  }
}

function popLayer() {
  if (layers.value.length > 1) {

    layers.value.pop();
    layerStore.popGrid();
    if (!layers.value.some(layer => layer === selectedLayer.value)) {
      selectedLayer.value = layers.value[layers.value.length - 1] ?? 1;
      layerStore.layer = selectedLayer.value;
    }
  }
}

function switchLayer(layer: number) {
  selectedLayer.value = layer;
  layerStore.layer = layer;
}

watch(() => props.updateLayers, () => {
  if (props.updateLayers) {
    selectedLayer.value = 0;
    layers.value.splice(0, layers.value.length);
    for (let i = 0; i < layerStore.grids.length; i++) {
      layers.value.push(i);
    }
  }
});
</script>

<style scoped>
  .selected-layer {
    background-color: #555 !important;
    color: white !important;
  }
</style>