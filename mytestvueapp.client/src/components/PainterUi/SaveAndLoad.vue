<template>
  <FloatingCard
    position="middle"
    width="25rem"
    header="Save and Load"
    button-icon="pi pi-save"
    button-label=""
    :default-open="false"
  >
    <div class="flex justify-content-around">
      <Button
        icon="pi pi-copy"
        label="Save to Clipboard"
        class="mr-2"
        @click="copyEncodedText()"
      />
      <Button
        icon="pi pi-upload"
        label="Load Painting"
        class="mr-2"
        @click="decodeToCanvas()"
        :disabled="stringEncodedText === ''"
      />
    </div>

    <Textarea
      class="mt-2 w-full"
      name="textArea"
      placeholder="Paste encoded text here!"
      v-model="stringEncodedText"
    />
  </FloatingCard>
</template>
<script setup lang="ts">
import { PixelGrid } from "@/entities/PixelGrid";
import FloatingCard from "./FloatingCard.vue";
import codec from "@/utils/codec";
import { ref } from "vue";
import Button from "primevue/button";
import Textarea from "primevue/textarea";
import { useToast } from "primevue/usetoast";

const toast = useToast();

var pixelGrid = defineModel<PixelGrid>();

const stringEncodedText = ref<string>("");

function copyEncodedText() {
  if (pixelGrid.value) {
    navigator.clipboard.writeText(codec.Encode(pixelGrid.value));
    toast.add({
      severity: "info",
      summary: "Info",
      detail: "Copied to clipboard",
      life: 3000,
    });
  } else {
    console.error("PixelGrid is undefined");
  }
}

function decodeToCanvas() {
  if (pixelGrid.value) {
    let backgroundColor = localStorage.getItem("backgroundColor");
    if (backgroundColor === null) {
      backgroundColor = "FFFFFF";
    }
    pixelGrid.value.updateGrid(
      codec.Decode(
        stringEncodedText.value,
        pixelGrid.value.width,
        pixelGrid.value.height,
        backgroundColor
      )
    );
  } else {
    console.error("PixelGrid is undefined");
  }
}
</script>
