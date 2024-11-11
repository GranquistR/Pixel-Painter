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
  <h1 class="px-4">Comments Section</h1>
  <div class="flex justify-content-center align-items-center"></div>
  <div class="border-solid border-1"></div>
  <h2 class="mb-2 ml-6">Add your own comment</h2>
  <textarea
    v-model:="commentBox" 
    class="ml-6" 
    placeholder="Write your comment...">
</textarea>
  <div><button class="mt-2 ml-6" @click=PostComment()>Post Comment</button></div>
  <CommentOnArt
    v-for="Comment in allComments"
    :key="Comment.commentId"
    :comment="Comment"
  ></CommentOnArt>
</template>
<script setup lang="ts">
const commentBox = ref("")
import Art from "@/entities/Art";
import MyCanvas from "@/components/MyCanvas/MyCanvas.vue";
import { ref, onMounted } from "vue";
import Comment from "@/entities/Comment";
import CommentOnArt from "@/components/Comment/CommentOnArt.vue";
import ArtAccessService from "../services/ArtAccessService";
import { useRoute } from "vue-router";
import CommentAccessService from "../services/CommentAccessService";
import LoginService from "@/services/LoginService";

const route = useRoute();

const allArt = ref<Art | null>(null);
const allComments = ref<Comment[] | null>(null);

onMounted(() => {
  const id = Number(route.params.id);

  ArtAccessService.getArtById(id).then((promise: Art) => {
    allArt.value = promise as Art;
  });
  CommentAccessService.getCommentsById(id).then((promise: Comment[]) => {
    allComments.value = promise;
  });
});
const PostComment = () => {
  if (commentBox.value != null){
    CommentAccessService.postComment(commentBox.value, Number(route.params.id));
  }
}
</script>
