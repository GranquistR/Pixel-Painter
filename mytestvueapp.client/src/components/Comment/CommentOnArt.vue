<template>
  <!-- <div class="m-2" v-if="Deleted == false">
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
  </div> -->

  <div class="flex align-items-center">
    <div class="flex-grow-1">
      <div class="flex gap-3">
        <span v-if="comment.currentUserIsOwner">
          <i class="pi pi-star-fill" style="color: yellow"></i>
        </span>
        <span style="font-weight: bold">{{ comment.commenterName }}</span>
        <span>{{ comment.creationDate }}</span>
      </div>
      <div class="mb-2 ml-2">
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
        <NewComment
          class="ml-4 mb-2"
          :comment="comment"
          @new-comment="emit('deleteComment')"
        ></NewComment>
        <!-- Show replies to comments -->
        <!-- <Button class="ml-3 mb-2" @click="">Show Replies</Button> -->
      </div>
    </div>
    <div style="width: 5rem">
      <Button
        v-if="comment.currentUserIsOwner"
        icon="pi pi-ellipsis-h"
        rounded
        text
        severity="secondary"
        @click="openMenu()"
      />
      <Menu ref="menu" :model="items" :popup="true" />
    </div>
  </div>
  <div class="ml-6 mb-2">
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
import { ref, watch } from "vue";
import Button from "primevue/button";
import InputText from "primevue/inputtext";
import Menu from "primevue/menu";
import { useToast } from "primevue/usetoast";
import NewComment from "./NewComment.vue";
import type ImageViewerView from "@/views/ImageViewerView.vue";

const emit = defineEmits(["deleteComment", "updateComments"]);

const editing = ref(false);
const newMessage = ref("");
const toast = useToast();
const allComments = ref<Comment[]>([]);

function openMenu() {
  menu.value.toggle(event);
}
const showReply = ref(true);
const menu = ref();
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

const props = defineProps<{
  comment: Comment;
}>();

//for textarea
const editBox = ref(props.comment.message); // needed to be able to edit comments

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

const updateComments = () => {};
</script>
