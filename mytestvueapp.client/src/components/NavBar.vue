<script setup>
    import { RouterLink } from 'vue-router';
    import { ref } from "vue";
    import Dialog from 'primevue/dialog';
    import InputText from 'primevue/inputtext';
    import Button from 'primevue/button';
    import Password from 'primevue/password';

    const visible = ref(false);
    const password = ref(null);
</script>

<!--<script src="https://apis.google.com/js/platform.js" async defer></script>
<meta name="google-signin-client_id" content="YOUR_CLIENT_ID.apps.googleusercontent.com">-->

<template>
    <div class="card flex justify-center">
        <Dialog v-model:visible="visible" modal header="Sign Into Profile" :style="{ width: '25rem' }" dismissableMask="false" @hide="password=''">
            <span class="text-surface-500 dark:text-surface-400 block mb-8">Input your information.</span>
            <div class="flex items-center gap-4 mb-4">
                <label for="username" class="font-semibold w-24">Username</label>
                <InputText id="username" class="flex-auto" autocomplete="off" />
            </div>
            <div class="flex items-center gap-4 mb-8">
                <label for="password" class="font-semibold w-24">Password</label>
                <!--<InputText id="password" class="flex-auto" autocomplete="off" />-->
                <Password v-model="password" :feedback="false" id="password" class="flex-auto" autocomplete="off" toggleMask/>
            </div>
            <div class="flex justify-end gap-2">
                <Button type="button" label="Cancel" severity="secondary" @click="visible = false; password = ''"></Button>
                <Button type="button" label="Log in" @click="visible = false; password = ''"></Button>
            </div>
        </Dialog>
    </div>

    <div class="container">
        <h1 class="page-name">Page Name</h1>
        <div class="options">
            <RouterLink active-class="active" to="/">Home</RouterLink>
            <RouterLink active-class="active" to="/paint">Painter</RouterLink>
            <RouterLink active-class="active" to="/gallery">Gallery</RouterLink>
        </div>
        <div>
            <img class="login" src="..\assets\profileImage.jpg" @click="visible = true" /><br />
        </div>
    </div>
</template>

<style scoped>
    .container {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 5px;
    }

    .active {
        border: 2px solid blue;
        font-weight: bolder;
    }

    a {
        text-decoration: none;
        border-radius: 10%;
        border: 2px solid black;
        padding: 5px;
    }

    .login {
        height: 25%;
        width: 25%;
        border-radius: 25px;
    }

</style>