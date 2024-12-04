<template>
  <div class="justify-content-center flex w-full h-full align-items-center">
    <div class="border-2">
      <my-canvas
        v-if="art"
        :key="art.id"
        :art="art"
        :pixelSize="20"
      ></my-canvas>
    </div>
    <Card class="w-20rem ml-5">
      <template #content>
        <h3 class="flex">
          {{ art.title }}
        </h3>

        <div>By {{ art.artistName }}</div>
        <div>Uploaded on {{ uploadDate.toLocaleDateString() }}</div>

        <div class="flex flex-row align-items-center gap-2 mt-4">
          <LikeButton class="" :art-id="id" :likes="art.numLikes"></LikeButton>
          <Button
            v-if="art.currentUserIsOwner"
            label="Edit"
            icon="pi pi-pencil"
            severity="secondary"
            @click="router.push(`/paint/${id}`)"
          ></Button>
          <DeleteArtButton v-if="art.currentUserIsOwner" :art="art">
          </DeleteArtButton>
        </div>
      </template>
    </Card>
  </div>

  <h2 class="px-4">{{ allComments.length }} Comments</h2>

  <div class="px-6">
    <NewComment @newComment="updateComments" class="mb-4"></NewComment>
    <CommentOnArt
      v-for="Comment in allComments"
      :key="Comment.id"
      :comment="Comment"
      @delete-comment="updateComments"
    ></CommentOnArt>
  </div>
</template>
<script setup lang="ts">
import DeleteArtButton from "@/components/DeleteArtButton.vue";
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
import Button from "primevue/button";
import router from "@/router";
import { useToast } from "primevue/usetoast";

const route = useRoute();
const toast = useToast();

const art = ref<Art>(new Art());
const allComments = ref<Comment[]>([]);
const id = Number(route.params.id);
const uploadDate = ref(new Date());

onMounted(() => {
  ArtAccessService.getArtById(id)
    .then((promise: Art) => {
      art.value = promise as Art;
      uploadDate.value = new Date(promise.creationDate);
    })
    .catch(() => {
      router.push("/gallery");
      toast.add({
        severity: "error",
        summary: "Error",
        detail: "Art not found",
        life: 3000,
      });
    });
  updateComments();
});

function updateComments() {
  CommentAccessService.getCommentsById(id).then((promise: Comment[]) => {
    allComments.value = promise;
  });
}
</script>
