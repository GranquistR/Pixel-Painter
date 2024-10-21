import { PixelGrid } from "@/entities/PixelGrid";

export default class codec {
  public static Encode(inputGrid: PixelGrid): string {
    let EncodedPicture: string = "";

    for (let i = 0; i < inputGrid.height; i++) {
      for (let j = 0; j < inputGrid.width; j++) {
        EncodedPicture = EncodedPicture + inputGrid.grid[i][j].substring(1);
      }
    }
    return EncodedPicture;
  }

  public static Decode(
    encodedString: String,
    height: number,
    width: number,
    backgroundColor: string

  ): PixelGrid {
    if (localStorage.getItem('backgroundColor')===null){
      backgroundColor = 'FFFFFF';
    }
    

    const decodedPicture: PixelGrid = new PixelGrid(height, width, backgroundColor);
    decodedPicture.createGrid(height, width, backgroundColor);
    let k = 0;

    if (encodedString === "") {
      return decodedPicture;
    }
    for (let i = 0; i < height; i++) {
      for (let j = 0; j < width; j++) {
        decodedPicture.grid[i][j] = "#" + encodedString.slice(k, k + 6);
        k = k + 6;
      }
    }

    return decodedPicture;
  }
}
