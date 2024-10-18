<template>
  <Button
    @click="openDialog = !openDialog"
    :label="buttonLabel"
    :severity="buttonSeverity"
    :icon="buttonIcon"
    class="mr-2"
  ></Button>
  <Dialog
    :position="position"
    v-model:visible="openDialog"
    :header="header"
    :style="{ width: width }"
  >
    <slot></slot>
  </Dialog>
</template>
<script setup lang="ts">
import Dialog from "primevue/dialog";
import Button from "primevue/button";
import { ref } from "vue";

const openDialog = ref(false);

defineProps<{
  position: string;
  header: string;
  width: string;
  buttonLabel: string;
  buttonSeverity: string;
  buttonIcon: string;
}>();
</script>
