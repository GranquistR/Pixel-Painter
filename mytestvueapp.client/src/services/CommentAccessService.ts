import Comment from "../entities/Comment";

export default class CommentAccessService {
  public static async getCommentsById(artId: number): Promise<any> {
    try {
      const response = await fetch(`/comment/GetCommentsById?id=${artId}`);
      console.log("GetComments-Response: ", response);
      const json = await response.json();
      console.log("GetComments-JSONData: ", json);

      const allComments: Comment[] = [];
      for (const comment of json) {
        allComments.push(comment as Comment);
      }

      console.log(allComments);
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
    public static async EditComment(commentId: number,newMessage:string): Promise<any> {
        try {
            console.log("this is called");
            const response = await fetch(
                `/comment/EditComment?commentId=${commentId}&newMessage=${newMessage}`,
            );
            console.log(response)

        } catch (error) {
            console.error;
        }
    }
}
