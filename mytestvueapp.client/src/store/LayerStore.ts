import { defineStore } from "pinia"
import { PixelGrid } from "@/entities/PixelGrid"

export const useLayerStore = defineStore('layers', {
  state: () => ({
    grids: [] as PixelGrid[],
    layer: 0
  }),
  actions: {
    init() {
      const store = localStorage.getItem('grids') as string;
      if (store) {
        const parsedGrids = JSON.parse(store);
        this.grids = parsedGrids.map((grid: any) => Object.assign(new PixelGrid(1, 1, "FF0000", false), grid));
      }
    },
    popGrid() {
      this.grids.pop();
    },
    save() {
      localStorage.setItem('grids', JSON.stringify(this.grids));
    },
    pushGrid(grid: PixelGrid) {
      this.grids.push(grid);
    },
    removeGrid(idx: number) {
      this.grids.splice(idx, 1);
    },
    empty() {
      this.grids.splice(0, this.grids.length);
    },
    clearStorage() {
      localStorage.removeItem('grids');
    }
  },
  getters: {
    getGrid: (state) => (gridIndex: number) => {
      return state.grids[gridIndex] || null;
    }
  }
});