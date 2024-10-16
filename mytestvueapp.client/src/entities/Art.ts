import type { PixelGrid } from "./PixelGrid";

export default class Art {
  artId?: number;
  artName?: string;
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
}
