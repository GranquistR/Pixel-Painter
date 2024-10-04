<template>
  <Dialog
    :position="position"
    :visible="true"
    :closable="false"
    :header="header"
    :style="{ width: width }"
  >
    <slot></slot>
  </Dialog>
</template>
<script setup lang="ts">
import Dialog from "primevue/dialog";
defineProps<{
  position: string;
  header: string;
  width: string;
}>();
</script>
