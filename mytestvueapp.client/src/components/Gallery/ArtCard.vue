<template>
  <div
    class="border-color mr-4 mb-4 border-round-md"
    @mouseenter="hover = true"
    @mouseleave="hover = false"
    :class="{ active: hover }"
  >
    <!-- Container -->
    <Card
      class="flex-shrink-0 w-13rem overflow-hidden border-round-md cursor-pointer p-0 gallery-card"
      @click="router.push(`/art/${art.artId}`)"
    >
      <template #header>
        <MyCanvas :art="art" :pixelSize="(32 / art.height) * 6.5" />
        <!-- <img class="w-full h-10rem m-0" :src="art.encode"/> -->
      </template>
      <template #title>
        <div class="text-base font-bold m-0 px-2 pt-1">
          {{ art.artName }}
        </div>
      </template>
      <template #subtitle>
        <div class="text-sm m-0 px-2">@{{ art.artistName }}</div>
      </template>
      <template #footer>
        <div class="flex flex-row w-full gap-2 mt-1 px-2 pb-2">
          <LikeButton :artId="props.art.artId" :likes="props.art.numLikes" />
          <Button
            class="w-full flex-grow p-2"
            icon="pi pi-comment"
            :label="art.numComments?.toString() || 'Null'"
          />
        </div>
      </template>
    </Card>
  </div>
</template>

<script setup lang="ts">
import Card from "primevue/card";
import Button from "primevue/button";
import LikeButton from "../LikeButton.vue";
import { ref } from "vue";
import MyCanvas from "../MyCanvas/MyCanvas.vue";
import Art from "@/entities/Art";
import router from "@/router";

const props = defineProps<{
  art: Art;
}>();

const hover = ref(false);
</script>

<style scoped>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}
.border-color {
  padding: 2px;
  background-color: var(--p-card-background);
  position: relative;
}
.active {
  background-color: var(--p-primary-color);
}
</style>

<style>
.gallery-card .p-card-body {
  padding: 0px !important;
  gap: 0px !important;
}

.gallery-card .p-card-caption {
  gap: 0px !important;
}
</style>
