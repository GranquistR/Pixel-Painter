<template>
  <FloatingCard position="bottom"
                header="Layers"
                button-icon="pi pi-clone"
                button-label=""
                width=""
                :default-open="true">

    <Button class="mr-1" v-if="layers.length>1" icon="pi pi-minus" size="small" rounded @click="popLayer()" />

    <template v-for="(layer, index) in layers" :key="layer.id">
      <Button :icon="layer.icon"
              :class="['m-1', { 'selected-layer': layer.id === selectedLayer } ]"
              severity="secondary"
              @click="switchLayer(layer.id)"
              @contextmenu.prevent="deleteLayer(layer.id)"/>
    </template>

    <Button class="ml-1" v-if="layers.length<8" icon="pi pi-plus" size="small" rounded @click="pushLayer()" />
  </FloatingCard>
</template>

<script setup lang="ts">
import Button from "primevue/button";
import FloatingCard from "./FloatingCard.vue";
import { ref } from 'vue';

let selectedLayer = defineModel<number>("selLayer", { default: 1 });

const layers = ref([
  { id: 1, icon: 'pi pi-stop' }
]);

function pushLayer() {
  if (layers.value.length < 8) {
    layers.value.push({
      id: layers.value.length+1,
      icon: 'pi pi-stop'
    });
  }
}

  function deleteLayer(id) {
    if (layers.value.length > 1) {
      const isConfirmed = confirm("This will delete layer " + id + ". Are you sure you want to delete this layer?");
      if (!isConfirmed) return;

      const index = layers.value.findIndex(layer => layer.id === id);

      if (index === -1) {
        console.warn(`Layer with id ${id} not found`);
        return;
      }
      layers.value.splice(index, 1);

      layers.value.forEach((layer, idx) => {
        layer.id = idx + 1;
      });
    }
  }

function popLayer() {
  if (layers.value.length > 1) {
    const isConfirmed = confirm("This will delete the top layer. Are you sure you want to delete this layer?");
    if (!isConfirmed) return;

    layers.value.pop();
    if (!layers.value.some(layer => layer.id === selectedLayer.value)) {
      selectedLayer.value = layers.value[layers.value.length - 1]?.id || 1;
    }
  }
}

function switchLayer(layerID: number) {
  selectedLayer.value = layerID;
}
</script>

<style>
  .selected-layer {
    background-color: #555 !important;
    color: white !important;
  }

  .p-dialog.p-component {
    margin-bottom: 75px !important;
  }
</style>