<template>
  <div>
    <div id="canvas" style="position: fixed"></div>
  </div>
</template>

<script setup lang="ts">
import { Application, Sprite, Texture } from "pixi.js";
import { Viewport } from "pixi-viewport"; // create viewport
import { onMounted, watch } from "vue";
import { PixelGrid } from "@/entities/PixelGrid";
import PainterTool from "@/entities/PainterTool";
import { Vector2 } from "@/entities/Vector2";
import Cursor from "@/entities/Cursor";

//Constants
var PIXEL_SIZE = 10;

//props
const props = defineProps<{
  pixelGrid: PixelGrid;
}>();

//exposes the recenter function to be called in parent component
defineExpose({ recenter });

//model
const cursor = defineModel<Cursor>({
  default: new Cursor(
    new Vector2(0, 0),
    PainterTool.getDefaults()[1],
    1,
    "#000000"
  ),
});

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
    }
  }
}

viewport.on("pointermove", (e) => {
  const pos = viewport.toWorld(e.globalX, e.globalY);

  cursor.value.position.x = Math.floor(pos.x / PIXEL_SIZE);
  cursor.value.position.y = Math.floor(pos.y / PIXEL_SIZE);
  updateCursor();
});

//update cursor, not full canvas
function updateCursor() {
  // Remove the old cursor
  viewport.children.forEach((child) => {
    if (child.alpha == 0.9) {
      viewport.removeChild(child);
    }
  });

  // Add the new cursor
  const cursorBox = new Sprite(Texture.WHITE);
  cursorBox.tint = "red";
  cursorBox.alpha = 0.9;
  cursorBox.width = cursor.value.size * PIXEL_SIZE;
  cursorBox.height = cursor.value.size * PIXEL_SIZE;
  cursorBox.position.set(
    cursor.value.position.x * PIXEL_SIZE,
    cursor.value.position.y * PIXEL_SIZE
  );
  if (cursor.value.position.x != -1 && cursor.value.position.y != -1) {
    viewport.addChild(cursorBox);
  }
}

//centers the canvas
function recenter() {
  viewport.fit();
  viewport.setZoom(40 / props.pixelGrid.width);
  viewport.moveCenter(
    (props.pixelGrid.width * PIXEL_SIZE) / 2,
    (props.pixelGrid.height * PIXEL_SIZE) / 2 + 80
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
  () => cursor.value.selectedTool.label,
  () => {
    //disable viewport drag if the tool is not the pan tool
    //but keep the .on click event
    checkIfPan();
  }
);

function checkIfPan() {
  if (cursor.value.selectedTool.label != "Pan") {
    viewport.threshold = 10000000;
  } else {
    viewport.threshold = 5;
  }
}
</script>
