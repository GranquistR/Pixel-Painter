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
import type Comment from "@/entities/Comment";

const props = defineProps<{
  comment: Comment;
}>();
</script>
