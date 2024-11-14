<template>
  <div class="justify-content-center flex w-full h-full align-items-center">
    <div class="border-2">
      <my-canvas
        v-if="art"
        :key="art.id"
        :art="art"
        :pixelSize="20 * (32 / art.pixelGrid.width)"
      ></my-canvas>
    </div>
    <Card class="w-20rem ml-5">
      <template #content>
        <div v-if="art.currentUserisOwner">this is shown</div>
        <h3>{{ art.title }}</h3>
        <div>By {{ art.artistName }}</div>
        <div>Uploaded on {{ uploadDate.toLocaleDateString() }}</div>
        <LikeButton
          class="mt-4"
          :art-id="id"
          :likes="art.numLikes"
        ></LikeButton>
      </template>
    </Card>
  </div>

  <h2 class="px-4">{{ allComments.length }} Comments</h2>

  <div class="px-6">
    <NewComment @newComment="updateComments"></NewComment>
    <CommentOnArt
      v-for="Comment in allComments"
      :key="Comment.id"
      :comment="Comment"
    ></CommentOnArt>
  </div>
</template>
<script setup lang="ts">
import Art from "@/entities/Art";
import MyCanvas from "@/components/MyCanvas/MyCanvas.vue";
import { ref, onMounted } from "vue";
import Comment from "@/entities/Comment";
import CommentOnArt from "@/components/Comment/CommentOnArt.vue";
import ArtAccessService from "../services/ArtAccessService";
import { useRoute } from "vue-router";
import CommentAccessService from "../services/CommentAccessService";
import NewComment from "@/components/Comment/NewComment.vue";
import Card from "primevue/card";
import LikeButton from "@/components/LikeButton.vue";
const route = useRoute();
const art = ref<Art>(new Art());
const allComments = ref<Comment[]>([]);
const id = Number(route.params.id);
const uploadDate = ref(new Date());
onMounted(() => {
  ArtAccessService.getArtById(id).then((promise: Art) => {
    art.value = promise as Art;
    uploadDate.value = new Date(promise.creationDate);
  });
  updateComments();
});
function updateComments() {
  CommentAccessService.getCommentsById(id).then((promise: Comment[]) => {
    allComments.value = promise;
  });
}
</script>
