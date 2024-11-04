import { PixelGrid } from "./PixelGrid";

export default class Art {
  artId: number;
  artName: string;
  artistId: number;
  artistName: string;
  width: number;
  height: number;
  encode: string;
  creationDate: string;
  isPublic: boolean;
  numLikes: number;
  numComments: number;
  pixelGrid: PixelGrid;

  constructor() {
    this.artId = 0;
    this.artName = "";
    this.artistId = 0;
    this.artistName = "";
    this.width = 0;
    this.height = 0;
    this.encode = "";
    this.creationDate = "";
    this.isPublic = false;
    this.numLikes = 0;
    this.numComments = 0;
    this.pixelGrid = new PixelGrid(1, 1, "FF0000");
  }
}
