<template>
  <div>
    <!-- {{ comment }} -->
    <InputText
      v-model:="comment.message"
      :placeholder="placeholder"
      class="w-full"
      @click="open = true"
      :disabled="!loggedIn"
      @keyup.enter="PostComment()"
    ></InputText>
    <div class="flex flex-row-reverse mt-2" v-if="open || parentComment">
      <Button class="ml-2" @click="PostComment()">Post Comment</Button>
      <Button severity="secondary" @click="Cancel()">Cancel</Button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from "vue";
import { useRoute } from "vue-router";
import CommentAccessService from "@/services/CommentAccessService";
import InputText from "primevue/inputtext";
import Button from "primevue/button";
import Comment from "@/entities/Comment";
import { useToast } from "primevue/usetoast";
import LoginService from "@/services/LoginService";

const toast = useToast();

const route = useRoute();
const open = ref(false);
const comment = ref<Comment>(new Comment());
const loggedIn = ref(false);

const emit = defineEmits(["newComment", "closeReply"]);

const placeholder = computed(() => {
  if (!loggedIn.value) {
    return "Login to comment";
  } else if (props.parentComment) {
    return "Reply to comment";
  } else {
    return "Add a comment...";
  }
});

function PostComment() {
  comment.value.artId = parseInt(route.params.id as string);
  comment.value.replyId = props.parentComment?.id;
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

onMounted(() => {
  LoginService.GetCurrentUser().then((user) => {
    if (user != null) {
      loggedIn.value = true;
    }
  });
});

const props = defineProps<{
  parentComment?: Comment;
}>();

function Cancel() {
  open.value = false;
  comment.value.message = "";
  emit("closeReply");
}
</script>
