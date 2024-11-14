import codec from "@/utils/codec";

export class PixelGrid {
  width: number;
  height: number;
  backgroundColor: string;
  grid: string[][];
  encodedGrid?: string;

  constructor(
    width: number,
    height: number,
    backgroundColor: string,
    encodedGrid?: string
  ) {
    this.width = width;
    this.height = height;
    this.grid = this.createGrid(width, height, backgroundColor);
    this.backgroundColor = backgroundColor;

    if (encodedGrid) {
      this.encodedGrid = encodedGrid;
      this.grid = codec.Decode(
        encodedGrid,
        height,
        width,
        backgroundColor
      ).grid;
    }
  }

  //Initialize a grid with a given width, height, and background color
  createGrid(
    width: number,
    height: number,
    backgroundColor: string
  ): string[][] {
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

  //Randomize the grid with random colors
  randomizeGrid(): void {
    for (let i = 0; i < this.height; i++) {
      for (let j = 0; j < this.width; j++) {
        this.grid[i][j] =
          "#" + ((Math.random() * 0xffffff) << 0).toString(16).padStart(6, "0");
      }
    }
    this.encodedGrid = codec.Encode(this);
  }

  //Update the grid with another grid
  DeepCopy(decodedGrid: PixelGrid): void {
    this.width = decodedGrid.width;
    this.height = decodedGrid.height;
    this.backgroundColor = decodedGrid.backgroundColor;
    this.grid = this.createGrid(this.width, this.height, this.backgroundColor);
    for (let i = 0; i < this.height; i++) {
      for (let j = 0; j < this.width; j++) {
        this.grid[i][j] = decodedGrid.grid[i][j];
      }
    }
    this.encodedGrid = codec.Encode(this);
  }

  public getEncodedGrid(): string {
    return codec.Encode(this);
  }
}
