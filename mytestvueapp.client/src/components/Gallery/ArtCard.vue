<script setup>
import Card from "primevue/card";
import Button from "primevue/button";
import { ref } from "vue";
import MyCanvas from "../MyCanvas/MyCanvas.vue";

const { art } = defineProps(["art"]);
const liked = ref(false);
const hover = ref(false);

const likes = ref("0");
const comments = ref("0");
console.log(art)

likes.value = `${art.numLikes}`;
if (art.numComments){
  comments.value = `${art.numComments}`;
}

const likedClicked = () => {
  liked.value = !liked.value;

  if (liked.value) {
    likes.value = art.numLikes + 1
  } else {
    likes.value = art.numLikes;
  }

  console.log(liked.value);
};


</script>

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
    >
      <template #header>
        <MyCanvas :art="art" :pixelSize="6.5"/>
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
          <Button
            :severity="liked ? 'danger' : ''"
            class="w-full flex-grow p-1"
            icon="pi pi-heart"
            :label="likes"
            @click="likedClicked()"
          />
          <Button 
          class="w-full flex-grow p-2" 
          icon="pi pi-comment" 
          :label="comments" />
        </div>
      </template>
    </Card>
  </div>
</template>

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
