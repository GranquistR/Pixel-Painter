<template>
  <div>
    <div id="canvas" style="position: fixed"></div>
  </div>
</template>

<script setup lang="ts">
import { Application, Sprite, Texture } from "pixi.js";
import { Viewport } from "pixi-viewport"; // create viewport
import { onMounted, ref, watch } from "vue";
import { PixelGrid } from "@/entities/PixelGrid";
import PainterTool from "@/entities/PainterTool";
import { Vector2 } from "@/entities/Vector2";

//Constants
var PIXEL_SIZE = 10;

//props
const props = defineProps<{
  pixelGrid: PixelGrid;
  selectedTool: PainterTool;
  selectedColor: string;
}>();

//exposes the recenter function to be called in parent component
defineExpose({ recenter });

//model
const cursorPosition = defineModel<Vector2>({ default: new Vector2(0, 0) });

//Runs on mounted, creates the canvas
onMounted(() => {
  document.getElementById("canvas")?.appendChild(app.view as any);
  drawCanvas();
  recenter();
  checkIfPan();
});

//initialize the canvas
const app = new Application({
  resizeTo: window,
  backgroundAlpha: 0,
});

// creates the viewport
var viewport = new Viewport({
  screenWidth: window.innerWidth,
  screenHeight: window.innerHeight,
  worldWidth: 100,
  worldHeight: 100,
  events: app.renderer.events,
});

// add the viewport to the stage
app.stage.addChild(viewport);

// activate viewport plugins
viewport.drag().pinch().wheel().decelerate({ friction: 0.7 });

//Draws the canvas
function drawCanvas() {
  viewport.removeChildren();

  for (var i = 0; i < props.pixelGrid.width; i++) {
    for (var j = 0; j < props.pixelGrid.height; j++) {
      const sprite = viewport.addChild(new Sprite(Texture.WHITE));
      sprite.tint = props.pixelGrid.grid[i][j];

      sprite.width = sprite.height = PIXEL_SIZE;
      sprite.position.set(i * PIXEL_SIZE, j * PIXEL_SIZE);
      sprite.interactive = true;
      sprite.on("pointerover", () => {
        cursorPosition.value.x = sprite.position.x / PIXEL_SIZE;
        cursorPosition.value.y = sprite.position.y / PIXEL_SIZE;
      });
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

//refresh canvas when pixelGrid changes
watch(props.pixelGrid.grid, () => {
  viewport.children.forEach((child) => {
    app.stage.removeChild(child);
  });
  drawCanvas();
});

//disable panning when not in pan mode
watch(
  () => props.selectedTool.label,
  () => {
    //disable viewport drag if the tool is not the pan tool
    //but keep the .on click event
    checkIfPan();
  }
);

function checkIfPan() {
  if (props.selectedTool.label != "Pan") {
    viewport.threshold = 10000000;
  } else {
    viewport.threshold = 5;
  }
}
</script>
