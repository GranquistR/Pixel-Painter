<template>
  <div
    class="shrink-limit flex justify-content-between align-items-center px-6 py-4"
  >
    <RouterLink class="router-link-unstyled" to="/">
      <h1 class="m-0 ml-2 font-bold">
        <span style="color: var(--p-primary-color)">Pixel</span>Painter
      </h1>
    </RouterLink>
    <div>
      <RouterLink class="p-2" to="/">
        <Button rounded label="Home" icon="pi pi-home" />
      </RouterLink>
      <RouterLink class="p-2" to="/test">
        <Button rounded label="Test" icon="pi pi-check" />
      </RouterLink>
      <RouterLink class="p-2" to="/paint">
        <Button rounded label="Painter" icon="pi pi-pencil" />
      </RouterLink>
      <RouterLink class="p-2" to="/gallery">
        <Button rounded label="Gallery" icon="pi pi-image" />
      </RouterLink>
      <RouterLink class="p-2" to="/imgtest">
        <Button rounded label="Img Test" icon="pi pi-camera" />
      </RouterLink>
    </div>
    <div>
      <DarkModeSwitcher class="mr-2" />
      <GoogleLogin></GoogleLogin>
    </div>
  </div>
</template>

<script setup>
import { RouterLink } from "vue-router";
import { ref } from "vue";
import Button from "primevue/button";
import DarkModeSwitcher from "./DarkModeToggle.vue";
import GoogleLogin from "../GoogleLogin.vue";

const visible = ref(false);
const password = ref(null);
</script>

<style scoped>
/* Prevents the UI from getting crushed. */
.shrink-limit {
  width: max(900px, 100%);
}
</style>
