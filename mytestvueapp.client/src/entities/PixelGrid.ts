import codec from "@/utils/codec";

export class PixelGrid {
  width: number;
  height: number;
  grid: string[][];


  constructor(width: number, height: number, backgroundColor: string, encodedGrid?: string) {
    this.width = width;
    this.height = height;
    this.grid = this.createGrid(width, height, backgroundColor);

    if (encodedGrid) {
      this.grid = codec.Decode(encodedGrid, height, width).grid;
    }

  }

  createGrid(width: number, height: number, backgroundColor: string): string[][] {
    const grid: string[][] = [];
    for (let i = 0; i < height; i++) {
      const row: string[] = [];
      for (let j = 0; j < width; j++) {
        row.push(backgroundColor);
      }
      grid.push(row);
    }
    return grid;
  }

  randomizeGrid(): void {
    for (let i = 0; i < this.height; i++) {
      for (let j = 0; j < this.width; j++) {
        this.grid[i][j] =
          "#" + ((Math.random() * 0xffffff) << 0).toString(16).padStart(6, "0");
      }
    }
  }

  updateGrid(decodedGrid: PixelGrid): void {
    for (let i = 0; i < this.height; i++) {
      for (let j = 0; j < this.width; j++) {
        this.grid[i][j] = decodedGrid.grid[i][j];
      }
    }
  }
}
