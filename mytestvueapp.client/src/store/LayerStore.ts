import { defineStore } from "pinia"
import { PixelGrid } from "@/entities/PixelGrid"

export const useLayerStore = defineStore('layers', {
  state: () => ({
    grids: [] as PixelGrid[]
  }),
  actions: {
    init() {
      const store = localStorage.getItem('grids');
      if (store) {
        this.grids = JSON.parse(store) as PixelGrid[];
      }
    },
    popGrid() {
      this.grids.pop();
    },
    save(art: PixelGrid) {
      this.grids[0].DeepCopy(art);
      localStorage.setItem('grids', JSON.stringify(this.grids));
    },
    pushGrid(grid: PixelGrid) {
      this.grids.push(grid);
    },
    removeGrid(id: number) {
      this.grids.splice(id, 1);
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