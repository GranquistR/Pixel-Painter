<template>
  <div class="m-2" v-if="Deleted == false">
    <Button
      severity="danger"
      v-if="editing == true"
      @click="DeleteComment(), (Deleted = true)"
      >Delete Comment</Button
    >
    <div class="flex gap-3">
      <span v-if="comment.currentUserIsOwner">
        <i class="pi pi-star-fill" style="color: yellow"></i>
      </span>
      <span style="font-weight: bold">{{ comment.commenterName }}</span>
      <span>{{ comment.creationDate }}</span>
    </div>
    <div class="mb-4 ml-2" v-if="editing == false && changedComment == false">
      <span>{{ comment.message }}</span>
    </div>
    <div class="mb-4 ml-2" v-if="editing == false && changedComment == true">
      <span>{{ editBox }}</span>
    </div>
    <div class="mb-4 ml-2" v-if="editing == true">
      <span><textarea placeholder="" v-model:="editBox"></textarea></span>
    </div>
    <div v-if="comment.currentUserIsOwner">
      <Button @click="editing = !editing" v-if="editing == false"
        >Edit Comment</Button
      >
      <Button
        v-if="editing == true"
        @click="SubmitEdit(), (editing = false), (changedComment = true)"
        >Submit</Button
      >
    </div>
  </div>
</template>

<script setup lang="ts">
import CommentAccessService from "../../services/CommentAccessService";
import type Comment from "@/entities/Comment";
import { ref } from "vue";
import Button from "primevue/button";

//constants for showing
const Deleted = ref(false);
const changedComment = ref(false);
const editing = ref(false);

const props = defineProps<{
  comment: Comment;
}>();

//for textarea
const editBox = ref(props.comment.message); // needed to be able to edit comments

const SubmitEdit = () => {
  if (props.comment.id != null && editBox.value != null) {
    if (editBox.value == "") {
      console.log("comment must contain something");
      return 0;
    }
    CommentAccessService.EditComment(props.comment.id, editBox.value);
  }
};
const DeleteComment = () => {
  if (props.comment.id != null) {
    CommentAccessService.DeleteComment(props.comment.id);
  }
};
</script>
