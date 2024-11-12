<template>
  <div>
    <InputText
      v-model:="comment.message"
      placeholder="Add a comment..."
      class="w-full"
      @click="open = true"
    ></InputText>
    <div class="flex flex-row-reverse mt-2" v-if="open">
      <Button class="ml-2" @click="PostComment()">Post Comment</Button>
      <Button severity="secondary">Cancel</Button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRoute } from "vue-router";
import CommentAccessService from "@/services/CommentAccessService";
import InputText from "primevue/inputtext";
import Button from "primevue/button";
import Comment from "@/entities/Comment";
import { useToast } from "primevue/usetoast";

const toast = useToast();

const route = useRoute();
const open = ref(false);
const comment = ref<Comment>(new Comment());

const emit = defineEmits(["newComment"]);

function PostComment() {
  comment.value.artId = parseInt(route.params.id as string);
  CommentAccessService.PostComment(comment.value)
    .then(() => {
      emit("newComment");
      Cancel();
    })
    .catch(() => {
      toast.add({
        severity: "error",
        summary: "Error",
        detail: "Failed to post comment",
        life: 3000,
      });
    });
}

function Cancel() {
  open.value = false;
  comment.value.message = "";
}
</script>
