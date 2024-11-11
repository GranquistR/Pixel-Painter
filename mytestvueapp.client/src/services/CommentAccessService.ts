import Comment from "../entities/Comment";

export default class CommentAccessService {
  public static async getCommentsById(artId: number): Promise<any> {
    try {
      const response = await fetch(`/comment/GetCommentsById?id=${artId}`);
      const json = await response.json();

      const allComments: Comment[] = [];
      for (const comment of json) {
        allComments.push(comment as Comment);
      }

      return allComments;
    } catch (error) {
      console.error;
    }
  }

  public static async isCookieCommentUser(artistId: string): Promise<any> {
    try {
      const response = await fetch(
        `/comment/CheckCookietoUser?commentUserId=${artistId}`,
      );
      const isMyComment: boolean = (await response.json()) as boolean;
      return isMyComment;
    } catch (error) {
      console.error;
    }
  }
  public static async EditComment(
    commentId: number,
    newMessage: string,
  ): Promise<any> {
    try {
      const response = await fetch(
        `/comment/EditComment?commentId=${commentId}&newMessage=${newMessage}`,
      );
    } catch (error) {
      console.error;
    }
  }
    public static async DeleteComment(commentId: number): Promise<any> {
        try {
            const response = await fetch(
                `/comment/DeleteComment?commentId=${commentId}`,
            );
        } catch (error) {
            console.error;
        }
    }
    public static async postComment(comment: string, ArtId: Number): Promise<any> {
        try {
            const response = await fetch(
                `/comment/postComment?comment=${comment}&ArtId=${ArtId}`,
            );
        } catch (error) {
            console.error;
        }
    }
}
