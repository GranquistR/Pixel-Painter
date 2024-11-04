<template>
  <div>
    <canvas :id="canvasId" class="vertical-align-middle"></canvas>
  </div>
</template>

<script setup lang="ts">
import type Art from "@/entities/Art";
import { onMounted, ref } from "vue";

const props = defineProps<{
  art: Art;
  pixelSize: number;
}>();

const canvasId = `viewer-page-canavs-${props.art.artId}`

const canvas = ref<HTMLCanvasElement>();
const context = ref<CanvasRenderingContext2D>();
//ctx.width =
onMounted(() => {
  let canvasInit = document.getElementById(
    canvasId,
  ) as HTMLCanvasElement;
  if (canvasInit) {
    canvas.value = canvasInit;
    let contextInit = canvasInit.getContext("2d");
    if (contextInit) {
      context.value = contextInit;
      canvas.value.width = props?.art?.width * props.pixelSize;
      canvas.value.height = props.art.height * props.pixelSize;
    }
  }

  render();
});
function render() {
  if (context.value) {
    const imageServe = props.art.encode;
    var hexBegin = 0;
    var hexEnd = 6;
    for (
      let column = 0;
      column < props.art.width * props.pixelSize;
      column += props.pixelSize
    ) {
      for (
        let row = 0;
        row < props.art.height * props.pixelSize;
        row += props.pixelSize
      ) {
        //console.log(b);
        context.value.fillStyle = "#" + imageServe.substring(hexBegin, hexEnd);
        context.value.fillRect(column, row, props.pixelSize, props.pixelSize);
        hexBegin += 6;
        hexEnd += 6;
      }
    }
  }
}
</script>
