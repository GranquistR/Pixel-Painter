export default class Artist {
  //required
  id: number;
  name: string;
  isAdmin: boolean;
  creationDate: string;

  constructor() {
    this.id = 0;
    this.name = "";
    this.isAdmin = false;
    this.creationDate = "";
  }
}
