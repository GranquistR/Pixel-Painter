<template>
  <div class="p-8 justify-content-center flex w-full h-full align-items-center">
    <div class="border-2">
      <my-canvas
        v-if="allArt"
        :key="allArt.artId"
        :art="allArt"
        :pixelSize="10"
      ></my-canvas>
    </div>
  </div>
  <div class="flex justify-content-center align-items-center"></div>
  <div class="border-solid border-1"></div>
  <CommentOnArt
    v-for="Comment in allComments"
    :key="Comment.commentId"
    :comment="Comment"
  ></CommentOnArt>
</template>
<script setup lang="ts">
import Art from "@/entities/Art";
import MyCanvas from "@/components/MyCanvas/MyCanvas.vue";
import { ref, onMounted } from "vue";
import Button from "primevue/button";
import ArtAccessService from "../utils/ArtAccessService";
import Comment from "@/entities/Comment";
import CommentOnArt from "@/components/Comment/CommentOnArt.vue";
const allArt = ref<Art | null>(null);
const allComments = ref<Comment[] | null>(null);

const MyImg = ref<Art>();
onMounted(() => {
  ArtAccessService.getArtById(2).then(
    (promise) => (allArt.value = promise as Art),
  );
  ArtAccessService.getCommentsById(2).then(
    (promise) => (allComments.value = promise as Comment[]),
  );
});
</script>
