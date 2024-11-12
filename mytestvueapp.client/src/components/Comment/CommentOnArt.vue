<template>
  <div class="m-2">
    <div class="flex gap-3">
      <span v-if="comment.currentUserIsOwner">
        <i class="pi pi-star-fill" style="color: yellow"></i>
      </span>
      <span style="font-weight: bold">{{ comment.commenterName }}</span>
      <span>{{ comment.creationDate }}</span>
    </div>
    <div class="mb-4 ml-2">
      <span>{{ comment.message }}</span>
    </div>
  </div>
</template>

<script setup lang="ts">
import CommentAccessService from "../../services/CommentAccessService";
import type Art from "@/entities/Art";
import type Comment from "@/entities/Comment";

//constants for showing
const Deleted = ref(false);
const artistIDisCookieUser = ref(false);
const changedComment = ref(false);
const showTextBox = ref(false);

const props = defineProps<{
  comment: Comment;
}>();
onMounted(() => {
  if (props.comment.artistId != null) {
    CommentAccessService.isCookieCommentUser(props.comment.artistId).then(
      (data) => {
        artistIDisCookieUser.value = data;
        return artistIDisCookieUser;
      },
    );
  }
});

//for textarea
const placehld = props.comment.commentContent; //to show message in the placeholder textarea
const editBox = ref(props.comment.commentContent); // needed to be able to edit comments

const SubmitEdit = () => {
  if (props.comment.commentId != null && editBox.value != null) {
    if (editBox.value == "") {
      console.log("comment must contain something");
      return 0;
    }
    CommentAccessService.EditComment(props.comment.commentId, editBox.value);
  }
};
const DeleteComment = () => {
  if (props.comment.commentId != null) {
    CommentAccessService.DeleteComment(props.comment.commentId);
  }
};
</script>
