<template>
  <Card>
    <template #title>Account</template>
    <template #content>
      <Button label="logout" icon="pi pi-sign-out" @click="logout()" />
    </template>
  </Card>
</template>

<script setup lang="ts">
import { onMounted } from "vue";
import LoginService from "@/services/LoginService";
import router from "@/router";
import { useToast } from "primevue/usetoast";
import Card from "primevue/card";
import Button from "primevue/button";

const toast = useToast();

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
  window.location.replace(`/`);
}
</script>
