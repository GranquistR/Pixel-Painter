<template>
  <Button
    label="Delete Art"
    icon=""
    @click="visible = !visible"
    severity="danger"
    class="block"
  ></Button>

  <Dialog v-model:visible="visible" modal :style="{ width: '26rem' }">
    <template #header>
      <h1 class="mr-2">Are you sure you want to delete your art?</h1>
    </template>
    <div class="flex flex-column gap-3 justify-content-center">
      <div class="flex align-items-center gap-3">
        <span>Reenter the name of the Title to delete: </span>
        <InputText
          v-model="confirmDeleteTitle"
          placeholder="Title"
          class="w-full"
        ></InputText>
      </div>
      <div class="flex align-items-center gap-3"></div>
    </div>
    <template #footer>
      <Button
        label="Cancel"
        text
        severity="secondary"
        @click="visible = false"
        autofocus
      />
      <Button
        label="Confirm"
        severity="secondary"
        @click="ConfirmDelete()"
        autofocus
      />
    </template>
  </Dialog>
</template>

<script setup lang="ts">
import Button from "primevue/button";
import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";
import { ref, watch } from "vue";
import Art from "@/entities/Art";
import ToggleButton from "primevue/togglebutton";
import ArtAccessService from "@/services/ArtAccessService";
import { useToast } from "primevue/usetoast";
import router from "@/router";
import LoginService from "@/services/LoginService";
const toast = useToast();
const visible = ref(false);
const loading = ref(false);
import { useRoute } from "vue-router";

const confirmDeleteTitle = ref("");
const emit = defineEmits(["OpenModal"]);
const route = useRoute();
const id = Number(route.params.id);
var matches = false;
function ConfirmDelete() {
  if (confirmDeleteTitle.value == "") {
    console.log("comment must contain something");
    return 0;
  }
  ArtAccessService.ConfirmDelete(id, confirmDeleteTitle.value).then(
    (promise: boolean) => {
      if (promise == true) {
        matches = true;
        ArtAccessService.DeleteArt(id);
        router.push("/gallery");
      }
    },
  );
}

watch(visible, () => {
  emit("OpenModal", visible.value);
});
</script>
