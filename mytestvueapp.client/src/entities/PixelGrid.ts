export class PixelGrid {
  width: number;
  height: number;
  grid: string[][];

  constructor(width: number, height: number) {
    this.width = width;
    this.height = height;
    this.grid = this.createGrid(width, height);
  }

  createGrid(width: number, height: number): string[][] {
    const grid: string[][] = [];
    for (let i = 0; i < height; i++) {
      const row: string[] = [];
      for (let j = 0; j < width; j++) {
        row.push("#ffffff");
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
}