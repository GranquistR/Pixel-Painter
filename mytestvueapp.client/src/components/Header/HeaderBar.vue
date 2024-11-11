<template>
  <Toolbar class="border-none custom-background">
    <template #start>
      <RouterLink class="router-link-unstyled" to="/">
        <h1 class="m-0 ml-2 font-bold">
          <span style="color: var(--p-primary-color)">Pixel</span>Painter
        </h1>
      </RouterLink>
    </template>
    <template #center>
      <div>
        <RouterLink class="p-2" to="/">
          <Button rounded label="Home" icon="pi pi-home" />
        </RouterLink>
        <RouterLink class="p-2" to="/new">
          <Button rounded label="Painter" icon="pi pi-pencil" />
        </RouterLink>
        <RouterLink class="p-2" to="/gallery">
          <Button rounded label="Gallery" icon="pi pi-image" />
        </RouterLink>
      </div>
    </template>
    <template #end>
      <DarkModeSwitcher class="mr-2" />
      <GoogleLogin :key="refreshKey" @refresh="refreshLogin" />
    </template>
  </Toolbar>
</template>

<script setup>
import { RouterLink } from "vue-router";
import { ref } from "vue";
import Button from "primevue/button";
import DarkModeSwitcher from "./DarkModeToggle.vue";
import GoogleLogin from "../GoogleLogin.vue";
import Toolbar from "primevue/toolbar";

const visible = ref(false);
const password = ref(null);
const refreshKey = ref(0);

const refreshLogin = () => {
  refreshKey.value++;
}
</script>

<style scoped>
/* Prevents the UI from getting crushed. */

.custom-background {
  background-color: transparent;
}
</style>
