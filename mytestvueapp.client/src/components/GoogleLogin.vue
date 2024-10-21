<template>
  <Button label="Login" @click="Login()" icon="pi pi-google"></Button>
</template>
<script setup lang="ts">
import Button from "primevue/button";

async function Login() {
  var url = new URL(window.location.href);
  window.location.replace(`login/Login?returnUrl=${url.origin}/LoginRedirect`);
}
</script>
