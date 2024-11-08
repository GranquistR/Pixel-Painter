<template>
  <Button
    label="Upload"
    icon="pi pi-upload"
    @click="visible = !visible"
  ></Button>

  <Dialog v-model:visible="visible" modal :style="{ width: '26rem' }">
    <template #header> <h1 class="mr-2">Upload Your Art</h1> </template>
    <div class="flex flex-column gap-3 justify-content-center">
      <div class="flex align-items-center gap-3">
        <span>Title: </span>
        <InputText
          v-model="art.title"
          placeholder="Title"
          class="w-full"
        ></InputText>
      </div>
      <div class="flex align-items-center gap-3">
        <span>Privacy:</span>
        <ToggleButton
          v-model="art.isPublic"
          onLabel="Public"
          onIcon="pi pi-globe"
          offLabel="Private"
          offIcon="pi pi-lock"
          class="w-36"
          aria-label="Do you confirm"
        />
        <span class="font-italic">*Visibility on gallery page*</span>
      </div>
    </div>
    <template #footer>
      <Button
        label="Cancel"
        text
        severity="secondary"
        @click="visible = false"
        autofocus
      />
      <Button label="Save" severity="secondary" @click="Upload()" autofocus />
    </template>
  </Dialog>
</template>

<script setup lang="ts">
import { PixelGrid } from "@/entities/PixelGrid";
import Button from "primevue/button";
import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";
import { ref } from "vue";
import Art from "@/entities/Art";
import ToggleButton from "primevue/togglebutton";
import ArtAccessService from "@/services/ArtAccessService";
import { useToast } from "primevue/usetoast";
import router from "@/router";
const toast = useToast();
const visible = ref(false);
const loading = ref(false);

const art = ref<Art>(new Art());
art.value.title = "Untitled Art";

const props = defineProps<{
  pixelGrid: PixelGrid;
}>();

function Upload() {
  loading.value = true;

  art.value.pixelGrid.DeepCopy(props.pixelGrid);

  ArtAccessService.UploadArt(art.value)
    .then((data: Art) => {
      if (data.id != undefined) {
        toast.add({
          severity: "success",
          summary: "Success",
          detail: "Art uploaded successfully",
        });
        localStorage.removeItem("working-art");
        router.push("/art/" + data.id);
      } else {
        toast.add({
          severity: "error",
          summary: "Error",
          detail: "Failed to upload art",
        });
      }
    })
    .catch((error) => {
      console.log(error);
      toast.add({
        severity: "error",
        summary: "Error",
        detail: "Failed to upload art",
      });
    })
    .finally(() => {
      loading.value = false;
      visible.value = false;
    });
}
</script>
