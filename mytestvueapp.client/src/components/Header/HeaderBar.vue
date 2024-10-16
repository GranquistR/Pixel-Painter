<script setup>
import { RouterLink } from "vue-router";
import { ref, onMounted } from "vue";
import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";
import Button from "primevue/button";
import Password from "primevue/password";
import DarkModeSwitcher from "./DarkModeToggle.vue";
import Avatar from "primevue/avatar";


    const visible = ref(false);

    // CSRF Token to compare to Google's
    /*const state = crypto.randomBytes(16).toString("hex");
    localStorage.setItem("CSRFToken", state);*/

    function redirectToLogin() {
        const clientId = '1030066146136-ld3kl9j8k7mllto6camk0psucphudvm8.apps.googleusercontent.com';
        const redirectURI = 'https://localhost:5173';
        const scope = 'email profile'; // Scopes the login to make sure the users email, name, and profile picture is obtained
        //const response = 'code';

        // const loginURL = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${clientId}&redirect_uri=${redirectURI}&scope=${scope}state=${state}&response_type=code`;
        const loginURL = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${clientId}&redirect_uri=${redirectURI}&scope=${scope}&response_type=code`;
        window.location.href = loginURL;
    }     

    onMounted(() => {
        const googleScript = document.createElement('script');
        googleScript.src = 'https://apis.google.com/js/platform.js';
        googleScript.async = true;
        document.body.appendChild(googleScript);

        const params = new URLSearchParams(window.location.search);
        const code = params.get('code');
        const state = params.get('state');

        console.log('Code: ', code);
        console.log('State: ', state);
    });
</script>

<template>
    <div>
        <Dialog v-model:visible="visible"
                modal
                header="Sign Into Profile"
                :style="{ width: '25rem' }"
                dismissableMask="false"
                @hide="password = ''">
            <span class="text-surface-500 dark:text-surface-400 block mb-8">Input your information.</span>
            <div class="flex items-center gap-4 mb-4">
                <label for="username" class="font-semibold w-24">Username</label>
                <InputText id="username" class="flex-auto" autocomplete="off" />
            </div>
            <div class="flex items-center gap-4 mb-8">
                <label for="password" class="font-semibold w-24">Password</label>
                <Password v-model="password"
                          :feedback="false"
                          id="password"
                          class="flex-auto"
                          autocomplete="off"
                          toggleMask />
            </div>
            <div class="flex justify-end gap-2">
                <Button type="button"
                        label="Cancel"
                        severity="secondary"
                        @click="
            visible = false;
            password = '';
          "></Button>
                <Button type="button"
                        label="Log in with Google"
                        @click="redirectToLogin">
                </Button>
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
      <RouterLink class="p-2" to="/"
        ><Button rounded label="Home" icon="pi pi-home"
      /></RouterLink>
      <RouterLink class="p-2" to="/test"
        ><Button rounded label="Test" icon="pi pi-check"
      /></RouterLink>
      <RouterLink class="p-2" to="/paint"
        ><Button rounded label="Painter" icon="pi pi-pencil"
      /></RouterLink>
      <RouterLink class="p-2" to="/gallery"
        ><Button rounded label="Gallery" icon="pi pi-image"
      /></RouterLink>
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
