<template>
  <div id="canvas" style="position: fixed"></div>
</template>

<script setup lang="ts">
import { Application, Sprite, Texture } from "pixi.js";
import { Viewport } from "pixi-viewport"; // create viewport
import { onMounted, ref, watch } from "vue";
import { PixelGrid } from "@/entities/PixelGrid";

const props = defineProps<{
  pixelGrid: PixelGrid;
}>();

defineExpose({ recenter });

//refs
var clickX = ref<number>(0);
var clickY = ref<number>(0);

//Constants
var PIXEL_SIZE = 10;

//Runs on mounted, creates the canvas
onMounted(() => {
  document.getElementById("canvas")?.appendChild(app.view as any);
  drawCanvas();
  recenter();
});

//initialize the canvas
const app = new Application({
  resizeTo: window,
  backgroundAlpha: 0,
});

var viewport = new Viewport({
  screenWidth: window.innerWidth,
  screenHeight: window.innerHeight,
  worldWidth: 100,
  worldHeight: 100,
  events: app.renderer.events,
});

// add the viewport to the stage
app.stage.addChild(viewport);

// activate plugins
viewport.drag().pinch().wheel().decelerate();

//Draws the canvas
function drawCanvas() {
  for (var i = 0; i < props.pixelGrid.width; i++) {
    for (var j = 0; j < props.pixelGrid.height; j++) {
      const sprite = viewport.addChild(new Sprite(Texture.WHITE));
      sprite.tint = props.pixelGrid.grid[i][j];

      sprite.width = sprite.height = PIXEL_SIZE;
      sprite.position.set(i * PIXEL_SIZE, j * PIXEL_SIZE);
    }
  }
}

//centers the canvas
function recenter() {
  viewport.fit();
  viewport.setZoom(50 / props.pixelGrid.width);
  viewport.moveCenter(
    (props.pixelGrid.width * PIXEL_SIZE) / 2,
    (props.pixelGrid.height * PIXEL_SIZE) / 2
  );
}

// HANDLERS
// Click handler
viewport.on("clicked", function (e) {
  clickX.value = Math.trunc(e.world.x / PIXEL_SIZE);
  clickY.value = Math.trunc(e.world.y / PIXEL_SIZE);
});

//refresh canvas handler
watch(props.pixelGrid.grid, () => {
  viewport.children.forEach((child) => {
    app.stage.removeChild(child);
  });

  drawCanvas();
});
</script>
