<template>
  <DrawingCanvas
    ref="canvas"
    :pixelGrid="pixelGrid"
    :style="{ cursor: cursor.selectedTool.cursor }"
    v-model="cursor"
    @mousedown="mouseButtonHeldDown = true"
    @mouseup="
      mouseButtonHeldDown = false;
      onMouseUp();
    "
    @contextmenu.prevent
  />
  <Toolbar class="fixed bottom-0 left-0 right-0 m-2">
    <template #start>
      <Button
        icon="pi pi-ban"
        label="Quit"
        severity="secondary"
        class="mr-2"
        @click="ResetArt()"
      >
      </Button>
      <UploadButton :pixelGrid="pixelGrid" @OpenModal="ToggleKeybinds" />
    </template>

    <template #center>
      <BrushSelection v-model="cursor.selectedTool" />
      <ColorSelection v-model:color="cursor.color" v-model:size="cursor.size" />
      <!-- <SaveAndLoad v-model="pixelGrid" /> -->
    </template>
    <template #end>
      <Button
        icon="pi pi-expand"
        class="mr-2"
        severity="primary"
        label="Recenter"
        @click="canvas?.recenter()"
      />
      <Button
        icon="pi pi-lightbulb"
        class="Rainbow"
        label="Give Me Color!"
        @click="pixelGrid.randomizeGrid()"
      />
    </template>
  </Toolbar>
</template>

<script setup lang="ts">
import Toolbar from "primevue/toolbar";
import DrawingCanvas from "@/components/PainterUi/DrawingCanvas.vue";
import { PixelGrid } from "@/entities/PixelGrid";
import { ref, watch, computed, onMounted, onUnmounted } from "vue";
import Button from "primevue/button";
import BrushSelection from "@/components/PainterUi/BrushSelection.vue";
import ColorSelection from "@/components/PainterUi/ColorSelection.vue";
import PainterTool from "@/entities/PainterTool";
import { Vector2 } from "@/entities/Vector2";
import Cursor from "@/entities/Cursor";
import router from "@/router";
import { onBeforeRouteLeave } from "vue-router";
import LinkedList from "@/utils/undo";
import DefaultColor from "@/entities/DefaultColors";
import UploadButton from "@/components/PainterUi/UploadButton.vue";

onBeforeRouteLeave((to, from, next) => {
  if (to.path != "/new" && !to.path.includes("/art")) {
    LocalSave();
  }
  next();
});

onMounted(() => {
  document.addEventListener("keydown", handleKeyDown);
});

onUnmounted(() => {
  document.removeEventListener("keydown", handleKeyDown);
});

const ToggleKeybinds = (disable: boolean) => {
  if (disable) {
    document.removeEventListener("keydown", handleKeyDown);
  } else {
    document.addEventListener("keydown", handleKeyDown);
  }
};

window.addEventListener("beforeunload", () => {
  LocalSave();
});

const cursor = ref<Cursor>(
  new Cursor(new Vector2(-1, -1), PainterTool.getDefaults()[1], 1, "#000000")
);
const mouseButtonHeldDown = ref<boolean>(false);

const workingGrid = JSON.parse(
  localStorage.getItem("working-art") as string
) as PixelGrid;

const pixelGrid = ref<PixelGrid>(
  new PixelGrid(
    workingGrid.width,
    workingGrid.height,
    workingGrid.backgroundColor
  )
);

if (workingGrid) {
  pixelGrid.value.DeepCopy(workingGrid);
}

const cursorPositionComputed = computed(
  //default vue watchers can't watch deep properties
  //it can only watch individual references to the object specified
  //since when cursor position changes, the object holding the cursor position doesn't change
  //thus the watcher won't trigger
  //in order to get around this, we can use a computed property
  //a computed propert is updated every time a dependency changes
  //this computed property will return a new object every time the cursor position changes
  //thus the watcher watching this value will trigger with the old and new values
  //vue likes to be funky like that :3
  () => new Vector2(cursor.value.position.x, cursor.value.position.y)
);

watch(
  cursorPositionComputed,
  (start: Vector2, end: Vector2) => {
    DrawAtCoords(GetLinePixels(start, end));
  },
  { deep: true }
);

watch(mouseButtonHeldDown, async () => {
  DrawAtCoords([cursor.value.position]);
});

function GetLinePixels(start: Vector2, end: Vector2): Vector2[] {
  const pixels: Vector2[] = [];

  const dx = Math.abs(end.x - start.x);
  const dy = Math.abs(end.y - start.y);

  const sx = start.x < end.x ? 1 : -1;
  const sy = start.y < end.y ? 1 : -1;

  let err = dx - dy;

  let currentX = start.x;
  let currentY = start.y;

  // eslint-disable-next-line no-constant-condition
  while (true) {
    pixels.push(new Vector2(currentX, currentY));

    // Check if we have reached the end point
    if (currentX === end.x && currentY === end.y) break;

    const e2 = 2 * err;

    if (e2 > -dy) {
      err -= dy;
      currentX += sx;
    }

    if (e2 < dx) {
      err += dx;
      currentY += sy;
    }
  }

  return pixels;
}

function DrawAtCoords(coords: Vector2[]) {
  coords.forEach((coord: Vector2) => {
    if (mouseButtonHeldDown.value) {
      if (cursor.value.selectedTool.label === "Brush") {
        for (let i = 0; i < cursor.value.size; i++) {
          for (let j = 0; j < cursor.value.size; j++) {
            if (
              coord.x + i >= 0 &&
              coord.x + i < pixelGrid.value.width &&
              coord.y + j >= 0 &&
              coord.y + j < pixelGrid.value.height
            ) {
              pixelGrid.value.grid[coord.x + i][coord.y + j] =
                cursor.value.color;
            }
          }
        }
      } else if (cursor.value.selectedTool.label === "Eraser") {
        for (let i = 0; i < cursor.value.size; i++) {
          for (let j = 0; j < cursor.value.size; j++) {
            if (
              coord.x + i >= 0 &&
              coord.x + i < pixelGrid.value.width &&
              coord.y + j >= 0 &&
              coord.y + j < pixelGrid.value.height
            ) {
              if (pixelGrid.value.backgroundColor != null) {
                pixelGrid.value.grid[coord.x + i][coord.y + j] =
                  pixelGrid.value.backgroundColor;
              }
            }
          }
        }
      } else if (
        coord.x >= 0 &&
        coord.x < pixelGrid.value.width &&
        coord.y >= 0 &&
        coord.y < pixelGrid.value.height
      ) {
        if (cursor.value.selectedTool.label === "Pipette") {
          cursor.value.color =
            pixelGrid.value.grid[cursor.value.position.x][
              cursor.value.position.y
            ];
        } else if (cursor.value.selectedTool.label === "Paint-Bucket") {
          if (pixelGrid.value.grid[coord.x][coord.y] != cursor.value.color) {
            fill(cursor.value.position.x, cursor.value.position.y);
          }
        }
      }
    }
  });
}

function fill(x: number, y: number) {
  if (y >= 0 && y < pixelGrid.value.height) {
    const oldColor = pixelGrid.value.grid[x][y];
    pixelGrid.value.grid[x][y] = cursor.value.color;
    if (oldColor != cursor.value.color) {
      if (x + 1 < pixelGrid.value.width) {
        if (pixelGrid.value.grid[x + 1][y] == oldColor) {
          //alert(x+1 + ", " + y);
          fill(x + 1, y);
        }
      }
      if (y + 1 < pixelGrid.value.height) {
        if (pixelGrid.value.grid[x][y + 1] == oldColor) {
          //alert(x + ", " + y+1);
          fill(x, y + 1);
        }
      }
      if (x - 1 >= 0) {
        if (pixelGrid.value.grid[x - 1][y] == oldColor) {
          //alert(x-1 + ", " + y);
          fill(x - 1, y);
        }
      }
      if (y - 1 >= 0) {
        if (pixelGrid.value.grid[x][y - 1] == oldColor) {
          //alert(x + ", " + (y-1));
          fill(x, y - 1);
        }
      }
    }
  }
}

function ResetArt() {
  localStorage.removeItem("working-art");
  router.push("/new");
}

var undoList = new LinkedList();
let currentGrid = JSON.parse(JSON.stringify(pixelGrid.value.grid));

undoList.append(currentGrid);

function onMouseUp() {
  currentGrid = JSON.parse(JSON.stringify(pixelGrid.value.grid));
  undoList.isDifferent(currentGrid);
}
function undo() {
  let previousGrid = undoList.getPrevious();
  if (previousGrid) {
    for (let i = 0; i < pixelGrid.value.width; i++) {
      for (let j = 0; j < pixelGrid.value.height; j++) {
        pixelGrid.value.grid[i][j] = previousGrid[i][j];
      }
    }
  }
}

function redo() {
  let nextGrid = undoList.getNext();
  if (nextGrid)
    for (let i = 0; i < pixelGrid.value.width; i++) {
      for (let j = 0; j < pixelGrid.value.height; j++) {
        pixelGrid.value.grid[i][j] = nextGrid[i][j];
      }
    }
}

function handleKeyDown(event: KeyboardEvent) {
  if (event.key === "p") {
    event.preventDefault();
    cursor.value.selectedTool.label = "Pan";
    canvas?.value.updateCursor();
  } else if (event.key === "b") {
    event.preventDefault();
    cursor.value.selectedTool.label = "Brush";
    canvas?.value.updateCursor();
  } else if (event.key === "e") {
    event.preventDefault();
    cursor.value.selectedTool.label = "Eraser";
    canvas?.value.updateCursor();
  } else if (event.key === "d") {
    event.preventDefault();
    cursor.value.selectedTool.label = "Pipette";
    canvas?.value.updateCursor();
  } else if (event.key === "f") {
    event.preventDefault();
    cursor.value.selectedTool.label = "Paint-Bucket";
    canvas?.value.updateCursor();
  } else if (event.key === "1") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[0].hex;
  } else if (event.key === "2") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[1].hex;
  } else if (event.key === "3") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[2].hex;
  } else if (event.key === "4") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[3].hex;
  } else if (event.key === "5") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[4].hex;
  } else if (event.key === "6") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[5].hex;
  } else if (event.key === "7") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[6].hex;
  } else if (event.key === "8") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[7].hex;
  } else if (event.key === "9") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[8].hex;
  } else if (event.key === "0") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[9].hex;
  } else if (event.key === "-") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[10].hex;
  } else if (event.key === "=") {
    event.preventDefault();
    cursor.value.color = DefaultColor.getDefaultColors()[11].hex;
  } else if (event.key === "q" && cursor.value.size > 1) {
    event.preventDefault();
    cursor.value.size -= 1;
    canvas?.value.updateCursor();
  } else if (event.key === "w" && cursor.value.size < 32) {
    event.preventDefault();
    cursor.value.size += 1;
    canvas?.value.updateCursor();
  } else if (event.ctrlKey && event.key === "z") {
    event.preventDefault();
    undo();
  } else if (event.ctrlKey && event.key === "y") {
    event.preventDefault();
    redo();
  }
}

function LocalSave() {
  localStorage.setItem("working-art", JSON.stringify(pixelGrid.value));
}

const canvas = ref();
</script>
<style scoped>
.Rainbow,
.Rainbow:hover {
  color: white;
  border-color: white;
  background: -webkit-linear-gradient(
      225deg,
      rgb(251, 175, 21),
      rgb(251, 21, 242),
      rgb(21, 198, 251)
    )
    0% 0% / 300% 300%;
  -webkit-animation: gradient_move 3s ease infinite;
  animation: gradient_move 3s ease infinite;
}
@-webkit-keyframes gradient_move {
  0% {
    background-position: 0% 92%;
  }
  50% {
    background-position: 100% 9%;
  }
  100% {
    background-position: 0% 92%;
  }
}

@keyframes gradient_move {
  0% {
    background-position: 0% 92%;
  }
  50% {
    background-position: 100% 9%;
  }
  100% {
    background-position: 0% 92%;
  }
}
</style>
