<template>
  <div class="w-full flex h-auto p-1 flex-nowrap">
    <div class="inline-block w-2 h-auto px-6 py-1 underline text-lg" id="User">
      {{ props.comment.artistName }}
    </div>
    <div
      class="inline-block text-left w-8 h-auto white-space-normal"
      id="Comment"
      v-if="showTextBox == false && changedComment == false"
    >
      {{ props.comment.commentContent }}
    </div>
    <div
      class="inline-block text-left w-8 h-auto white-space-normal"
      id="Comment"
      v-if="showTextBox == false && changedComment == true"
    >
      {{ editBox }}
    </div>
    <textarea
      placeholder="[[placehld]]"
      v-model:="editBox"
      class="inline-block text-left w-8 h-auto white-space-normal"
      v-if="artistIDisCookieUser == true && showTextBox == true"
    ></textarea>
    <div class="inline-block w-2 h-auto px-3" id="Timestamp ">
      {{ props.comment.commentTime }}
    </div>
    <button
      v-if="artistIDisCookieUser == true && showTextBox == true"
      @click="SubmitEdit(), (showTextBox = false), (changedComment = true)"
    >
      Submit
    </button>
    <button
      v-if="artistIDisCookieUser == true"
      @click="showTextBox = !showTextBox"
    >
      edit comment
    </button>
  </div>
</template>

<script setup lang="ts">
import CommentAccessService from "../../services/CommentAccessService";
import type Art from "@/entities/Art";
import type Comment from "@/entities/Comment";
import { onMounted, reactive, ref, watch } from "vue";
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
const placehld = props.comment.commentContent;
const editBox = ref(props.comment.commentContent);

const SubmitEdit = () => {
  if (props.comment.commentId != null && editBox.value != null) {
    CommentAccessService.EditComment(props.comment.commentId, editBox.value);
  }
};
</script>
