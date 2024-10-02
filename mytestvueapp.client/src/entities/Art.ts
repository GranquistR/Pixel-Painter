
export default class Art {
  id: number
  title: string
  artistName: string
  image: string

  constructor(id: number, title: string, artistName: string, image: string) {
    this.id = id
    this.title = title
    this.artistName = artistName
    this.image = image
  }
}