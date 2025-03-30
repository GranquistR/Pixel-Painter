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
import { useLayerStore } from "@/store/LayerStore.ts"
import { PixelGrid } from "@/entities/PixelGrid.ts"

import { ref, defineEmits, onBeforeMount } from 'vue';

const layerStore = useLayerStore();
const selectedLayer = ref<number>(1);
const layers = ref<number[]>([1]);

const props = defineProps<{ pixelGrid: PixelGrid }>();
const emit = defineEmits(['change-layer']);

onBeforeMount(() => {
  for (let i = 1; i < layerStore.grids.length; i++)
    layers.value.push(i + 1);
});

function pushLayer() {
  if (layers.value.length < 8) {
    layers.value.push(layers.value.length + 1);
    layerStore.pushGrid(new PixelGrid(
      props.pixelGrid.width,
      props.pixelGrid.height,
      props.pixelGrid.backgroundColor,
      props.pixelGrid.isGif)
    );
    console.log(layerStore.grids);
  }
}

function deleteLayer(id) {
  if (layers.value.length > 1) {
    const isConfirmed = confirm("This will delete layer " + id + ". Are you sure you want to delete this layer?");
    if (!isConfirmed) return;

    let idx = id - 1;

    if (idx === -1) {
      console.warn(`Layer with id ${id} not found`);
      return;
    }
    layers.value.splice(idx, 1);
    layerStore.removeGrid(idx);

    if (!layers.value.some(layer => layer === selectedLayer.value)) {
      layers.value.forEach((layer, index) => {
        layers.value[index] = index + 1;
      });
    }
    selectedLayer.value = idx - 1;
    emit('change-layer', selectedLayer.value);
  }
}

function popLayer() {
  if (layers.value.length > 1) {

    layers.value.pop();
    layerStore.popGrid();
    if (!layers.value.some(layer => layer === selectedLayer.value)) {
      selectedLayer.value = layers.value[layers.value.length - 1] ?? 1;
    }
    emit('change-layer', selectedLayer.value - 1);
  }
}

function switchLayer(layer) {
  selectedLayer.value = layer;
  emit('change-layer', selectedLayer.value-1);
}
</script>

<style scoped>
  .selected-layer {
    background-color: #555 !important;
    color: white !important;
  }
</style>