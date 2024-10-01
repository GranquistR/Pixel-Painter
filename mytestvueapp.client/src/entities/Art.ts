
export default class Art { 
  ArtId: number 
  ArtName: string 
  ArtistId: number
  Width: number
  ArtLength: number
  Encode: string
  CreationDate: string
  IsPublic: boolean

  constructor(
    ArtId: number, 
    ArtName: string, 
    ArtistId: number, 
    Width: number, 
    ArtLength: number, 
    Encode: string,
    CreationDate: string,
    IsPublic: boolean
  ) {
    this.ArtId = ArtId
    this.ArtName = ArtName
    this.ArtistId = ArtistId
    this.Width = Width
    this.ArtLength = ArtLength
    this.Encode = Encode
    this.CreationDate = CreationDate
    this.IsPublic = IsPublic
  }
}