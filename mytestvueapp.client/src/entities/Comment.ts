export default class Comment {
  id?: number;
  artistId?: number;
  artId?: number;
  message?: string;
  commenterName?: string;
  creationDate?: string;
  replyId?: number;
  currentUserIsOwner?: boolean;

  constructor() {
    this.id = 0;
    this.artistId = 0;
    this.artId = 0;
    this.message = "";
    this.commenterName = "unknown";
    this.creationDate = "";
    this.replyId = 0;
    this.currentUserIsOwner = false;
  }
}
