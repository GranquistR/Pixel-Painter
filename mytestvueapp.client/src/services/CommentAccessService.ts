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
}
