<template>
  <Button
    rounded
    :icon="checked ? 'pi pi-sun' : 'pi pi-moon'"
    @click="checked = !checked"
  ></Button>
</template>
<script setup lang="ts">
import Button from "primevue/button";
import { onMounted, ref, watch } from "vue";

const checked = ref(false);

watch(checked, () => {
  if (checked.value) {
    const element = document.querySelector("html");
    element?.classList.toggle("dark-mode-toggle");
  } else {
    const element = document.querySelector("html");
    element?.classList.toggle("dark-mode-toggle");
  }
});

onMounted(() => {
  const element = document.querySelector("html");
  element?.classList.toggle("dark-mode-toggle");
});
</script>
