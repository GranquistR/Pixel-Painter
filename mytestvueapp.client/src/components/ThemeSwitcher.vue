<template>
  <Card style="width: 20rem; overflow: hidden">
    <template #title>Theming</template>
    <template #content>
      <Button @click="newTheme" class="mr-2">Switch Theme</Button>

      <Button @click="darkMode">Dark Mode</Button>
    </template>
  </Card>
</template>

<script setup lang="ts">
import { updatePreset } from "@primevue/themes";
import Button from "primevue/button";
import Card from "primevue/card";

function newTheme() {
  updatePreset({
    semantic: {
      primary: {
        //#2f2f2f : #cb39b1

        50: "{indigo.50}",
        100: "{indigo.100}",
        200: "{indigo.200}",
        300: "{indigo.300}",
        400: "{indigo.400}",
        500: "{indigo.500}", //primary color
        600: "{indigo.600}",
        700: "{indigo.700}",
        800: "{indigo.800}",
        900: "{indigo.900}",
        950: "{indigo.950}",
      },
    },
  });
}

function darkMode() {
  const element = document.querySelector("html");
  element?.classList.toggle("dark-mode-toggle");
}
</script>
