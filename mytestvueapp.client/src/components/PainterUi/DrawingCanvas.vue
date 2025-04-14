<template>
  <div>
    <div id="canvas" style="position: fixed"></div>
  </div>
</template>

<script setup lang="ts">
import { Application, Sprite, Texture } from "pixi.js";
import { DropShadowFilter } from "@pixi/filter-drop-shadow";
import { OutlineFilter } from "@pixi/filter-outline";
import { Viewport } from "pixi-viewport"; // create viewport
import { onMounted, watch, ref } from "vue";
import { PixelGrid } from "@/entities/PixelGrid";
import PainterTool from "@/entities/PainterTool";
import { Vector2 } from "@/entities/Vector2";
import Cursor from "@/entities/Cursor";
import { useLayerStore } from "@/store/LayerStore.ts"



//Constants
const PIXEL_SIZE = 10;
const layerStore = useLayerStore();

//props
const props = defineProps<{
  grid: PixelGrid
}>();

//exposes (only put methods here if there are things painterview does that DIRECTLY update the canvas)
defineExpose({ recenter, updateCursor, drawLayers, updateCell, init, drawFrame, updateCellFrame });

//model
const cursor = defineModel<Cursor>({
  default: new Cursor(
    new Vector2(0, 0),
    PainterTool.getDefaults()[1],
    1,
    "#000000",
  ),
});

//Runs on mounted, creates the canvas
onMounted(() => {
  document.getElementById("canvas")?.appendChild(app.view as any);
  init();
  drawLayers(0);
  recenter();
  checkIfPan();
});

//initialize the canvas
const app = new Application({
  resizeTo: window,
  backgroundAlpha: 0,
});

// creates the viewport
const viewport = new Viewport({
  screenWidth: window.innerWidth,
  screenHeight: window.innerHeight,
  worldWidth: 100,
  worldHeight: 100,
});

// add the viewport to the stage
app.stage.addChild(viewport);

// activate viewport plugins
viewport.drag().pinch().wheel().decelerate({ friction: 0.7 });

//Creates dropshadow and background layer
function init() {
  viewport.removeChildren();

  var dropShadowFilter = new DropShadowFilter();
  dropShadowFilter.distance = 0;

  const dropShadow = new Sprite(Texture.WHITE);
  dropShadow.width = layerStore.grids[0].width * PIXEL_SIZE;
  dropShadow.height = layerStore.grids[0].width * PIXEL_SIZE;
  dropShadow.tint = 0x000000;
  dropShadow.filters = [dropShadowFilter];

  const background = new Sprite(Texture.WHITE);
  background.width = layerStore.grids[0].width * PIXEL_SIZE;
  background.height = layerStore.grids[0].width * PIXEL_SIZE;
  background.tint = layerStore.grids[0].backgroundColor;

  viewport.addChild(dropShadow);
  viewport.addChild(background);
}

function drawLayers(layer: number) {
  let dropShadow = viewport.children[0];
  let background = viewport.children[1];
  background.tint = layerStore.grids[layer].backgroundColor;
  viewport.removeChildren();
  viewport.addChild(dropShadow);
  viewport.addChild(background);

  let width = layerStore.grids[0].width;
  let height = layerStore.grids[0].height;
  for (let length = 0; length < layerStore.grids.length; length++) {
    for (let i = 0; i < width; i++) {
      for (let j = 0; j < height; j++) {
        const sprite = viewport.addChild(new Sprite(Texture.WHITE));
        if (layerStore.grids[length].grid[i][j] === "empty") {
          sprite.tint = layerStore.grids[length].backgroundColor;
          sprite.alpha = 0;
        } else {
          sprite.tint = layerStore.grids[length].grid[i][j];
          sprite.alpha = 1;
        }
        sprite.width = sprite.height = PIXEL_SIZE;
        sprite.position.set(i * PIXEL_SIZE, j * PIXEL_SIZE);
        sprite.interactive = (length === layer) ? true : false; //reduce lag
      }
    }
    if (length === layer) break;
  }
}

    function drawFrame(frame: number) {
        let dropShadow = viewport.children[0];
        let background = viewport.children[1];
        background.tint = layerStore.grids[frame].backgroundColor;
        viewport.removeChildren();
        viewport.addChild(dropShadow);
        viewport.addChild(background);

        let width = layerStore.grids[0].width;
        let height = layerStore.grids[0].height;
        for (let i = 0; i < width; i++) {
            for (let j = 0; j < height; j++) {
                const sprite = viewport.addChild(new Sprite(Texture.WHITE));
                if (layerStore.grids[frame].grid[i][j] === "empty") {
                    sprite.tint = layerStore.grids[frame].backgroundColor;
                    sprite.alpha = 0;
                } else {
                    sprite.tint = layerStore.grids[frame].grid[i][j];
                    sprite.alpha = 1;
                }
                sprite.width = sprite.height = PIXEL_SIZE;
                sprite.position.set(i * PIXEL_SIZE, j * PIXEL_SIZE);
                // sprite.interactive = (length === frame) ? true : false; //reduce lag
            }
        }
    }

function updateCell(layer: number, x: number, y: number, color: string) {
  let idx = layerStore.grids[0].width ** 2 * layer + 2;
  idx += (x * layerStore.grids[0].width + y);
  if (color === "empty") {
    viewport.children[idx].alpha = 0;
  } else {
    viewport.children[idx].tint = color;
    if (layer <= layerStore.layer) viewport.children[idx].alpha = 1;
    else viewport.children[idx].alpha = 0;
  }
}

    function updateCellFrame(frame: number, x: number, y: number, color: string) {
        let idx = (x * layerStore.grids[0].width + y + 2);

        if (color === "empty") {
            viewport.children[idx].alpha = 0;
        } else {
            viewport.children[idx].tint = color;
            viewport.children[idx].alpha = 1;
        }
    }

const pos = ref<any>();

viewport.on("pointermove", (e) => {
  pos.value = viewport.toWorld(e.globalX, e.globalY);

  updateCursor();
});

//update cursor, not full canvas
function updateCursor() {
  if (
    cursor.value.selectedTool.label == "Brush" ||
    cursor.value.selectedTool.label == "Eraser"
  ) {
    cursor.value.position.x = Math.floor(
      (pos.value.x - ((cursor.value.size - 1) / 2) * PIXEL_SIZE) / PIXEL_SIZE,
    );
    cursor.value.position.y = Math.floor(
      (pos.value.y - ((cursor.value.size - 1) / 2) * PIXEL_SIZE) / PIXEL_SIZE,
    );
  } else {
    cursor.value.position.x = Math.floor(pos.value.x / PIXEL_SIZE);
    cursor.value.position.y = Math.floor(pos.value.y / PIXEL_SIZE);
  }

  // Remove the old cursor
  viewport.children.forEach((child) => {
    if (child.alpha == 0.99) {
      viewport.removeChild(child);
    }
  });

  // Add the new cursor
  const cursorBox = new Sprite(Texture.WHITE);
  var outlineFilter = new OutlineFilter(1, 0x000000);
  outlineFilter.knockout = true;
  cursorBox.alpha = 0.99;

  if (
    cursor.value.selectedTool.label == "Brush" ||
    cursor.value.selectedTool.label == "Eraser"
  ) {
    cursorBox.width = cursor.value.size * PIXEL_SIZE;
    cursorBox.height = cursor.value.size * PIXEL_SIZE;
    cursorBox.position.set(
      cursor.value.position.x * PIXEL_SIZE,
      cursor.value.position.y * PIXEL_SIZE,
    );
  } else {
    cursorBox.width = PIXEL_SIZE;
    cursorBox.height = PIXEL_SIZE;
    cursorBox.position.set(
      cursor.value.position.x * PIXEL_SIZE,
      cursor.value.position.y * PIXEL_SIZE,
    );
  }

  cursorBox.filters = [outlineFilter];
  viewport.addChild(cursorBox);
}

//centers the canvas
function recenter() {
  viewport.fit();
  viewport.setZoom(40 / layerStore.grids[0].width);
  viewport.moveCenter(
    (layerStore.grids[0].width * PIXEL_SIZE) / 2,
    (layerStore.grids[0].height * PIXEL_SIZE) / 2 + layerStore.grids[0].height * 2.5,
  );
}

watch(() => props.grid.backgroundColor, (prev, next) => {
  if (viewport.children[1].tint !== props.grid.backgroundColor) {
    viewport.children[1].tint = props.grid.backgroundColor;
  }
});

//disable panning when not in pan mode
watch(
  () => cursor.value.selectedTool.label,
  () => {
    //disable viewport drag if the tool is not the pan tool
    //but keep the .on click event
    checkIfPan();
  },
);

function checkIfPan() {
  if (cursor.value.selectedTool.label != "Pan") {
    viewport.threshold = 10000000;
  } else {
    viewport.threshold = 5;
  }
}
</script>
