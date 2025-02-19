<template>
  <div class="flex align-items-center">
    <div class="flex-grow-1">
      <div class="flex gap-3">
        <span v-if="comment.currentUserIsOwner">
          <i class="pi pi-star-fill" style="color: yellow"></i>
        </span>
        <span style="font-weight: bold">{{ comment.commenterName }}</span>
        <span>{{ comment.creationDate }}</span>
      </div>
      <div class="ml-2">
        <span v-if="!editing" style="word-break: break-word">{{
          comment.message
        }}</span>
        <div v-else>
          <InputText
            v-model:="newMessage"
            placeholder="Add a comment..."
            class="w-full mt-2"
          ></InputText>
          <div class="flex flex-row-reverse mt-2 gap-2">
            <Button
              label="Submit"
              @click="SubmitEdit"
              :disabled="newMessage == ''"
            ></Button>
            <Button
              label="Cancel"
              severity="secondary"
              @click="editing = false"
            ></Button>
          </div>
        </div>
      </div>
      <div>
        <!-- Reply to comments -->
        <Button
          @click="showReply = true"
          icon="pi pi-comment"
          rounded
          text
          label="Reply"
          severity="secondary"
        ></Button>
        <Button
          v-if="comment.currentUserIsOwner || user"
          icon="pi pi-ellipsis-h"
          rounded
          text
          severity="secondary"
          @click="openMenu()"
        />
        <NewComment
          v-if="showReply == true"
          class="ml-4 mb-2"
          :parent-comment="comment"
          @new-comment="emit('deleteComment'), (showReply = false)"
          @close-reply="showReply = false"
        ></NewComment>

        <!-- Show replies to comments -->
        <!-- <Button class="ml-3 mb-2" @click="">Show Replies</Button> -->
      </div>
    </div>
    <div style="width: 5rem">
      <Menu ref="menu" :model="items" :popup="true" />
    </div>
  </div>
  <div class="ml-4 pl-3 mb-2" style="border-left: solid 1px gray">
    <CommentOnArt
      v-for="Comment in comment.replies"
      :key="Comment.id"
      :comment="Comment"
      @delete-comment="emit('deleteComment')"
    ></CommentOnArt>
  </div>
</template>

<script setup lang="ts">
import CommentAccessService from "../../services/CommentAccessService";

import type Comment from "@/entities/Comment";
import { ref, watch, onMounted } from "vue";
import Button from "primevue/button";
import InputText from "primevue/inputtext";
import Menu from "primevue/menu";
import { useToast } from "primevue/usetoast";
import NewComment from "./NewComment.vue";
import LoginService from "../../services/LoginService";

const emit = defineEmits(["deleteComment", "updateComments"]);

const editing = ref(false);
const newMessage = ref("");
const toast = useToast();
const showReply = ref(false);
const menu = ref();
const user = ref<boolean>(false);

function openMenu() {
  menu.value.toggle(event);
}

const items = ref([
  {
    label: "Delete",
    icon: "pi pi-trash",
    command: () => {
      DeleteComment();
    },
  },
  {
    label: "Edit",
    icon: "pi pi-pencil",
    command: () => {
      editing.value = true;
    },
  },
]);

watch(editing, () => {
  newMessage.value = props.comment.message ?? "";
});

onMounted(() => {
  getIsAdmin();
});
const props = defineProps<{
  comment: Comment;
}>();

function getIsAdmin() {
  LoginService.GetIsAdmin().then((promise: boolean) => {
    user.value = promise;
  });
}

const SubmitEdit = () => {
  if (props.comment.id != null) {
    CommentAccessService.EditComment(props.comment.id, newMessage.value)
      .then(() => {
        emit("deleteComment");
        editing.value = false;
      })
      .catch(() => {
        toast.add({
          severity: "error",
          summary: "Error",
          detail: "Failed to edit comment",
          life: 3000,
        });
      });
  }
};

const DeleteComment = () => {
  if (props.comment.id != null) {
    CommentAccessService.DeleteComment(props.comment.id).then(() => {
      emit("deleteComment");
    });
  }
};
</script>
