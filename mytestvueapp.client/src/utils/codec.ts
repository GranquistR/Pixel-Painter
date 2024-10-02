import { PixelGrid } from "@/entities/PixelGrid";

export default class codec{
  
public static Encode(inputGrid: PixelGrid): void{
    let EncodedPicture: string ="";

    for (let i = 0; i < inputGrid.height; i++) {
      for (let j = 0; j < inputGrid.width; j++) {
        EncodedPicture = EncodedPicture + inputGrid.grid[i][j].substring(1);
          
      }
    }
    console.log(EncodedPicture);
  }

  public static Decode(encodedString: String, height: number, width: number): PixelGrid{
    const decodedPicture: PixelGrid = new PixelGrid(height, width);

    if (encodedString === "") {
        decodedPicture.createGrid(height, width);
        return decodedPicture;
    }

    decodedPicture.createGrid(height, width);

    for (let i = 0; i < height; i++) {
      for (let j = 0; j < width; j++) {
        decodedPicture.grid[i][j]="#" + encodedString.slice(0,6);
        encodedString.substring(6);
      }
    }
    return decodedPicture;
  }
}
