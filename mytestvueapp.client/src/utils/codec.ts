import { PixelGrid } from "@/entities/PixelGrid";
import PainterView from "@/views/PainterView.vue"


export default class codec {
  public static Encode(inputGrid: PixelGrid): string {
    let EncodedPicture: string = "";
      EncodedPicture = EncodedPicture + '(' + localStorage.getItem('backgroundColor') + ')';
      //EncodedPicture = EncodedPicture + '[' + localStorage.getItem('resolution') + ']';
    for (let i = 0; i < inputGrid.height; i++) {
      for (let j = 0; j < inputGrid.width; j++) {
        EncodedPicture = EncodedPicture + inputGrid.grid[i][j];
      }
    }
    
      let newStr = "";
    
      for (let i = 0; i < EncodedPicture.length; i++) {
        if (EncodedPicture[i] !== '#') {
          newStr += EncodedPicture[i];
        }
      }
    
      return newStr;
    }
    
  

  public static Decode(
    encodedString: string,
    height: number,
    width: number,
    backgroundColor: string


    
  ): PixelGrid {
    
    
    function getStringBetweenParentheses(str: string): string {
      const regex = /\(([^)]+)\)/;
      const match = regex.exec(str);

      if (match && match[1]) {
        return match[1];
      } else {
        return ""; // Return an empty string if no match is found
      }
    }
    function cutStringAfterCharacter(str: string, char: string): string {
      const parts = str.split(char);
      return parts[1]; 
    }
   
    backgroundColor = getStringBetweenParentheses(encodedString);
    
    localStorage.setItem('backgroundColor',backgroundColor );
    
      console.log(localStorage.getItem('backgroundColor'));
    
    encodedString = cutStringAfterCharacter(encodedString,')');
    
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
