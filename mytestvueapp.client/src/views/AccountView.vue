<template>
  <Card>
    <template #title>Account</template>
    <template #content>
        <IftaLabel>
            <InputText id="username" v-model="value" variant="filled" :disabled="isDisabled" placeholder="Username" />
            <label for="username">Username</label>
            <Button label="Edit" @click="toggleInput()" />
            <Button label="Gen User" @click="getNewUsername()" />
        </IftaLabel>
        <div> <Button label="logout" icon="pi pi-sign-out" @click="logout()" /></div>
    </template>
  </Card>
</template>
    
<script setup lang="ts">
import { ref, onMounted } from "vue";
import LoginService from "@/services/LoginService";
import router from "@/router";
import { useToast } from "primevue/usetoast";
import Card from "primevue/card";
    import Button from "primevue/button";
    import InputText from 'primevue/inputtext';
    import IftaLabel from 'primevue/iftalabel';

    const toast = useToast();
    var isDisabled = ref(true);



onMounted(() => {
  LoginService.isLoggedIn().then((isLoggedIn) => {
    if (!isLoggedIn) {
      router.push("/");
      toast.add({
        severity: "error",
        summary: "Warning",
        detail: "User must be logged in to view account page",
        life: 3000,
      });
    }
  });
});

function logout() {
  LoginService.logout().then(() => {
    router.push("/");
    toast.add({
      severity: "success",
      summary: "Success",
      detail: "User logged out",
      life: 3000,
    });
  });
    }

    function toggleInput() {
        isDisabled.value = !isDisabled.value;
    }

    function getNewUsername() {
        // console.log(LoginService.generateUsername());
        return LoginService.generateUsername();
    }
</script>   
