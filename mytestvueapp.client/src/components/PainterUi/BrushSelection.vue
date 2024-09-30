<template>
  <Dialog
    position="right"
    :visible="true"
    :closable="false"
    header="Tool Select"
    :style="{ width: '25rem' }"
  >
    <div v-for="tool in tools" :key="tool.label" class="flex">
      <Button
        class="w-full mb-2"
        :severity="tool !== selectedTool ? 'secondary' : 'primary'"
        @click="selectedTool = tool"
      >
        <span :class="tool.icon" class="mr-2"></span>
        <span>
          {{ tool.label }}
        </span>
        <span class="flex-grow-1"></span>
        <Tag severity="secondary" :value="tool.shortcut"></Tag>
      </Button>
    </div>
  </Dialog>
</template>
<script setup lang="ts">
import { ref } from "vue";
import Button from "primevue/button";
import Tag from "primevue/tag";
import Dialog from "primevue/dialog";

const tools = ref([
  { label: "Brush", icon: "pi pi-pencil", shortcut: "b" },
  { label: "Pan", icon: "pi pi-arrows-alt", shortcut: "p" },
  { label: "Eraser", icon: "pi pi-eraser", shortcut: "e" },
]);

var selectedTool = ref(tools.value[0]);
</script>
