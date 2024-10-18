<template>
  <div class="w-auto flex h-auto p-1 flex-nowrap">
    <div
      class="inline-block w-auto h-auto px-6 py-1 underline text-lg"
      id="User"
    >
      {{ user }}
    </div>
    <div class="inline-block w-auto h-auto white-space-normal" id="Comment">
      {{ usercomment }}
    </div>
    <div class="inline-block w-auto h-auto px-3" id="Timestamp ">
      {{ time }}
    </div>
  </div>
</template>

<script setup lang="ts">
import type Art from "@/entities/Art";
import { onMounted, ref } from "vue";
// let usercomment = prop.comment
// let user = prop.ArtistName
// let time = prop.CommentTime

const props = defineProps<{
  comment: Comment;
}>();
</script>
