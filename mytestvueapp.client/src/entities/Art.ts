export default class Art {
  artId: number;
  artName: string;
  artistId: number;
  width: number;
  artLength: number;
  encode: string;
  creationDate: string;
  isPublic: boolean;

  constructor(
    artId: number,
    artName: string,
    artistId: number,
    width: number,
    artLength: number,
    encode: string,
    creationDate: string,
    isPublic: boolean
  ) {
    this.artId = artId;
    this.artName = artName;
    this.artistId = artistId;
    this.width = width;
    this.artLength = artLength;
    this.encode = encode;
    this.creationDate = creationDate;
    this.isPublic = isPublic;
  }
}
