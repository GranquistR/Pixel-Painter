<template>
  <FloatingCard position="right" header="Tool Select" width="25rem">
    <div
      v-for="tool in PainterTool.getDefaults()"
      :key="tool.label"
      class="flex"
    >
      <Button
        class="w-full mb-2"
        :severity="tool.label !== model.label ? 'secondary' : 'primary'"
        @click="model = tool"
      >
        <span :class="tool.icon" class="mr-2"></span>
        <span>
          {{ tool.label }}
        </span>
        <span class="flex-grow-1"></span>
        <Tag severity="secondary" :value="tool.shortcut"></Tag>
      </Button>
    </div>
  </FloatingCard>
</template>

<script setup lang="ts">
import Button from "primevue/button";
import Tag from "primevue/tag";
import FloatingCard from "./FloatingCard.vue";
import PainterTool from "@/entities/PainterTool";

const model = defineModel<PainterTool>({
  default: PainterTool.getDefaults()[0],
});
</script>
