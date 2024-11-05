<template>
<!--   
  <div class="w-full flex h-auto p-1 flex-nowrap">
    <div
      class="inline-block w-2 h-auto px-7 py-2 underline text-lg"
      id="User"
    >
      {{ props.comment.artistName }}
    </div>
    <div class="inline-block text-left w-8 h-auto white-space-normal" id="Comment">
      {{ props.comment.commentContent }}
    </div> -->
    <div class="m-2 px-6">

      <div class="flex gap-3">
        
        <span style="font-weight: bold;">{{ comment.artistName }}</span>
        <span>{{ comment.commentTime }}</span>
      </div>
      <div class="mb-4 ml-2">
        
        <span >{{ comment.commentContent }}</span>
      </div>
    </div>
</template>

<script setup lang="ts">
import type Art from "@/entities/Art";
import type Comment from "@/entities/Comment";
import { onMounted, ref } from "vue";

const props = defineProps<{
  comment: Comment;
}>()
</script>
