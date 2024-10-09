<script setup>
import { RouterLink } from "vue-router";
import { ref } from "vue";
import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";
import Button from "primevue/button";
import Password from "primevue/password";
import DarkModeSwitcher from "./DarkModeToggle.vue";
import Avatar from "primevue/avatar";

const visible = ref(false);
const password = ref(null);
</script>

<!--<script src="https://apis.google.com/js/platform.js" async defer></script>
<meta name="google-signin-client_id" content="YOUR_CLIENT_ID.apps.googleusercontent.com">-->

<template>
  <div>
    <Dialog
      v-model:visible="visible"
      modal
      header="Sign Into Profile"
      :style="{ width: '25rem' }"
      dismissableMask="false"
      @hide="password = ''"
    >
      <span class="text-surface-500 dark:text-surface-400 block mb-8"
        >Input your information.</span
      >
      <div class="flex items-center gap-4 mb-4">
        <label for="username" class="font-semibold w-24">Username</label>
        <InputText id="username" class="flex-auto" autocomplete="off" />
      </div>
      <div class="flex items-center gap-4 mb-8">
        <label for="password" class="font-semibold w-24">Password</label>
        <Password
          v-model="password"
          :feedback="false"
          id="password"
          class="flex-auto"
          autocomplete="off"
          toggleMask
        />
      </div>
      <div class="flex justify-end gap-2">
        <Button
          type="button"
          label="Cancel"
          severity="secondary"
          @click="
            visible = false;
            password = '';
          "
        ></Button>
        <Button
          type="button"
          label="Log in"
          @click="
            visible = false;
            password = '';
          "
        ></Button>
      </div>
    </Dialog>
  </div>

  <div class="flex justify-content-between align-items-center px-6 py-4">
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
      <DarkModeSwitcher />
      <Button
        class="ml-2"
        iconPos="right"
        icon="pi pi-user"
        label="Login"
        @click="visible = !visible"
      />
    </div>
  </div>
</template>
