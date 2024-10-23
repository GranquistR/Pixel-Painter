import { PixelGrid } from "./PixelGrid";

export default class Art {
  artId?: number;
  artName: string;
  artistId?: number;
  artistName?: string;
  width?: number;
  artLength?: number;
  encode?: string;
  creationDate?: string;
  isPublic?: boolean;
  numLikes?: number;
  numComments?: number;
  pixelGrid?: PixelGrid;
  
  constructor(
    artId: number, 
    artName: string, 
    artistId: number, 
    artistName: string,
    width: number, 
    artLength: number, 
    encode: string,
    creationDate: string,
    isPublic: boolean,
    numLikes: number,
    numComments: number,
  ) {
    this.artId = artId
    this.artName = artName
    this.artistId = artistId
    this.artistName = artistName
    this.width = width
    this.artLength = artLength
    this.encode = encode
    this.creationDate = creationDate
    this.isPublic = isPublic
    this.numLikes = numLikes
    this.numComments = numComments
    this.pixelGrid = new PixelGrid(width,artLength,encode)
  }
}
