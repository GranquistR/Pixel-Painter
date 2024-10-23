<template>
  <div class="w-full flex h-auto p-1 flex-nowrap">
    <div
      class="inline-block w-2 h-auto px-6 py-1 underline text-lg"
      id="User"
    >
      {{ props.comment.artistName }}
    </div>
    <div class="inline-block text-left w-8 h-auto white-space-normal" id="Comment">
      {{ props.comment.commentContent }}
    </div>
    <div class="inline-block w-2 h-auto px-3" id="Timestamp ">
      {{ props.comment.commentTime }}
    </div>
  </div>
</template>

<script setup lang="ts">
import type Art from "@/entities/Art";
import type Comment from "@/entities/Comment";
import { onMounted, ref } from "vue";

const props = defineProps<{
  comment: Comment;
}>();
</script>
