<template>
  <div class="p-8 justify-content-center flex w-full h-full align-items-center">
    <div class="border-2">
      <my-canvas></my-canvas>
    </div>
  </div>
  <div class="border-solid border-1">
    <h2>"this is some text"</h2>
  </div>
</template>
<script setup lang="ts">
import Art from "@/entities/Art";
import MyCanvas from "@/components/MyCanvas/MyCanvas.vue";
import { ref, onMounted } from "vue";
import Button from "primevue/button";

const MyImg = ref<Art>();
onMounted(() => {});
</script>
