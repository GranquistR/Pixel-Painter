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
import Comment from "@/entities/Comment";
import CommentOnArt from "@/components/Comment/CommentOnArt.vue";
import ArtAccessService from "../services/ArtAccessService";
import { useRoute } from "vue-router";

const route = useRoute();

const allArt = ref<Art | null>(null);
const allComments = ref<Comment[] | null>(null);

onMounted(() => {
  const id = Number(route.params.id);

  ArtAccessService.getArtById(id).then((promise: Art) => {
    allArt.value = promise as Art;
  });
  ArtAccessService.getCommentsById(id).then((promise: Comment[]) => {
    allComments.value = promise;
  });
});
</script>
