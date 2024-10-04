<script setup lang="ts">
import HeaderBar from "@/components/Header/HeaderBar.vue";
import { RouterView } from "vue-router";
import Toast from "primevue/toast";
</script>

<template>
  <Toast />

  <HeaderBar />
  <RouterView />
</template>
