<template>
  <div
    class="absolute bottom-50 bg-primary flex align-items-center justify-content-center w-full h-10rem"
  >
    <Card
      class="flex ml-auto mr-3 h-12rem align-items-center justify-content-start"
    >
      <template #title>Enter Title And Description Of Art</template>
      <template #content>
        <InputText
          class="mt-2 w-full"
          type="text"
          v-model="title"
          placeholder="Title"
        />
        <Textarea
          class="mt-2 w-full"
          name="textArea"
          placeholder="Description"
          style="resize: none"
          v-model="description"
        />
      </template>
    </Card>

    <Card
      class="flex mr-auto ml-3 w-auto h-12rem align-items-center justify-content-start"
    >
      <template #title>Select Background Color And Canvas Resolution</template>

      <template #content>
        <label for="backgroundColorPick">Background Color: </label>
        <ColorPicker
          class="w-auto p-2"
          v-model="backgroundColor"
          id="backgroundColorPick"
          format="hex"
        ></ColorPicker>
        <br />
        <label for="resolution">Resolution: </label>
        <InputNumber
          class="w-auto p-2"
          id="resolution"
          v-model="resolution"
          showButtons
          buttonLayout="horizontal"
          suffix=" px"
          :min="1"
          :max="64"
        >
          <template #incrementbuttonicon>
            <span class="pi pi-plus" />
          </template>
          <template #decrementbuttonicon>
            <span class="pi pi-minus" />
          </template>
        </InputNumber>
      </template>
    </Card>
  </div>
  <div
    class="absolute bottom-0 bg-primary flex align-items-center justify-content-center w-full h-10rem"
  >
    <Button
      rounded
      label="Start Painting"
      icon="pi pi-pencil"
      @click="updateLocalStorage()"
    ></Button>
  </div>
</template>
<script setup lang="ts">
import Button from "primevue/button";
import ColorPicker from "primevue/colorpicker";
import Card from "primevue/card";
import InputNumber from "primevue/inputnumber";
import Textarea from "primevue/textarea";
import InputText from "primevue/inputtext";
import Art from "@/entities/Art";
import { onMounted, ref } from "vue";
import router from "@/router";
import { PixelGrid } from "@/entities/PixelGrid";

const resolution = ref<number>(32);
const title = ref<string>("");
const description = ref<string>("");
const backgroundColor = ref<string>("#ffffff");

function updateLocalStorage() {
  var pixelGrid = new PixelGrid(
    resolution.value,
    resolution.value,
    backgroundColor.value
  );

  localStorage.setItem("working-art", JSON.stringify(pixelGrid));

  router.push("/paint");
}

onMounted(() => {
  var art = localStorage.getItem("working-art");
  if (art !== null) {
    router.push("/paint");
  }
});
</script>
<style>
.p-colorpicker-preview {
  width: 7rem !important;
  height: 2rem !important;
  border: solid 1px !important;
}
</style>
